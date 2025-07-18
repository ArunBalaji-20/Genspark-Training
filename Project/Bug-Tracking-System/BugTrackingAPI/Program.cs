using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using BugTrackingAPI.Context;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Repositories;
using BugTrackingAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BugTrackingAPI.Misc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BugTrackingAPI.Hubs;
using System.Threading.RateLimiting;
using Serilog;
using Microsoft.Extensions.FileProviders;
using Serilog.Sinks.AzureBlobStorage;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//storing logs in azure blob , set aspnetcore environment=development and then run 
var useAzureBlob = builder.Configuration.GetValue<bool>("Storage:UseAzureBlob");
System.Console.WriteLine(useAzureBlob);
if (useAzureBlob)
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.AzureBlobStorage(
            connectionString: builder.Configuration["Storage:ConnectionString"],
            storageContainerName: builder.Configuration["Storage:logs"],
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
           
        )
        .CreateLogger();
}
else
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
}

// Configure Serilog
// Log.Logger = new LoggerConfiguration()
//     .Enrich.FromLogContext()
//     .WriteTo.Console()
//     .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

#region key vault
// var keyVaultUrl = builder.Configuration["AzureBlob:KeyVaultUrl"];
// if (!string.IsNullOrWhiteSpace(keyVaultUrl))
// {
//     builder.Configuration.AddAzureKeyVault(
//         new Uri(keyVaultUrl),
//         new DefaultAzureCredential());
// }
// var blobConnStr = builder.Configuration["sac-3"];
// builder.Configuration["Storage:ConnectionString"] = blobConnStr;
// System.Console.WriteLine(blobConnStr);
#endregion

builder.Host.UseSerilog();

#region API Versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader(); // /v{version}/controller
})
.AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'VVV"; // e.g., v1, v2
    opt.SubstituteApiVersionInUrl = true;
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

#region connection string from azure vault

var keyVaultUrl = builder.Configuration["AzureBlob:KeyVaultUrl"];
if (!string.IsNullOrWhiteSpace(keyVaultUrl))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(keyVaultUrl),
        new DefaultAzureCredential());
}


var dbConnectionString = builder.Configuration["PostgresConnectionString"];
#endregion

#region Dbconfiguration
builder.Services.AddDbContext<BugContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));  //from local
    // opts.UseNpgsql(dbConnectionString); //from azure vault
});
System.Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
#endregion

#region Repositories
builder.Services.AddTransient<IRepository<long, Employee>, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IRepository<int, RefreshTokenModel>, RefreshTokenRepository>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<IRepository<long, Bugs>, BugsRepository>();
builder.Services.AddTransient<IRepository<long, BugAssignment>, BugsAssignmentRepository>();
builder.Services.AddTransient<IRepository<long, BugComment>, BugCommentRepository>();
builder.Services.AddTransient<IRepository<long, BlackListedToken>, BlackListRepository>();
builder.Services.AddTransient<IBlackListRepository, BlackListRepository>();
builder.Services.AddTransient<IBugManagementRepository, BugsAssignmentRepository>();
#endregion

#region Services
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IBugService, BugService>();
builder.Services.AddTransient<IBugManagementService, BugManagementService>();
builder.Services.AddTransient<IBugCommentService, BugCommentService>();
builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementService>();
#endregion
// if (useAzureBlob)
// {
//     builder.Services.AddTransient<IScreenshotStorageService, AzureScreenshotStorageService>();
// }


#region Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtTokenKey"]))
                    };
                options.Events = new JwtBearerEvents
{
    OnMessageReceived = context =>
    {
        // Default behavior
        return Task.CompletedTask;
    },
    OnTokenValidated = async context =>
    {
        var blackListRepo = context.HttpContext.RequestServices.GetRequiredService<IBlackListRepository>();
        var jwtToken = context.SecurityToken as JwtSecurityToken;
        var tokenString = context.HttpContext.Request.Headers["Authorization"]; 
        if (!string.IsNullOrEmpty(tokenString))
        {
            var tokenStr = tokenString.ToString().Substring("Bearer ".Length).Trim();
        // System.Console.WriteLine($"Token String: {tokenStr}");

            var isBlacklisted = await blackListRepo.IsBlacklistedAsync(tokenStr);
            if (isBlacklisted)
            {
                context.Fail("This token has been revoked.");
            }
        }
    }
};
                });
#endregion

#region jsonserializer
builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });
#endregion

#region cors
// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(policy =>
//     {
//         policy.WithOrigins("http://127.0.0.1:5500" , "http://127.0.0.1:4200")
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .AllowCredentials();
//     });
// });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                new Uri(origin).Host == "127.0.0.1" || new Uri(origin).Host == "localhost"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion


builder.Services.AddSignalR();

#region RateLimiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 1000, //allows only 1000 request per minute on all endpoints
                QueueLimit = 0,
                Window = TimeSpan.FromHours(1)
            }));
});
#endregion



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"BUG TRACKING API {description.GroupName.ToUpperInvariant()}"
            );
        }
    });
}

#region running miragrations on docker env
if (builder.Configuration["RUN_MIGRATIONS"] == "true")
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BugContext>();
    db.Database.Migrate();
    if (!db.Employees.Any())
    {
        db.Employees.Add(new Employee
        {
            Name = "Admin",
            Email = "admin@gmail.com",
            Role = "Admin",
            PasswordHash="$2a$11$wW0pknOYdn2XcbgQzjFtp.EBvGu/q7m8suqbRSbkS.v2UWkqHRMJ6"
            
        });

        db.SaveChanges();
    }
}
#endregion

app.UseMiddleware<RequestLoggingMiddleware>();

// app.UseHttpsRedirection();
// app.UseRouting();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "screenshots")),
    RequestPath = "/screenshots"
});

app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();
app.Run();
