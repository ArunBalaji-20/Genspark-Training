using FirstAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using FirstAPI.Repositories;
using FirstAPI.Models;
using FirstAPI.Interfaces;
using FirstAPI.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
// builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddTransient<IRepository<int, Doctor>, DoctorRepository>();
builder.Services.AddTransient<IRepository<int, Speciality>, SpecialityRepository>();        
builder.Services.AddTransient<IRepository<int, DoctorSpeciality>, DoctorSpecialityRepository>();
// builder.Services.AddTransient<IRepository<int, Patient>, PatinetRepository>();
// builder.Services.AddTransient<IRepository<int, Speciality>, SpecialityRepository>();
// builder.Services.AddTransient<IRepository<string, Appointmnet>, AppointmnetRepository>();
// builder.Services.AddTransient<IRepository<int, DoctorSpeciality>, DoctorSpecialityRepository>();

builder.Services.AddTransient<IDoctorService, DoctorService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClinicContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
