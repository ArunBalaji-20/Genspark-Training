

using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using videoportalAPI.Context;
using videoportalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Env.Load();
System.Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
#region Dbconfiguration
builder.Services.AddDbContext<VideosContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region services
builder.Services.AddTransient<VideoService>();
#endregion

#region 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
{
    policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

});
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();


