using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TwoeterApi;
using TwoeterApi.Model.Repository;
using TwoeterApi.Model.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

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

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
