using Newtonsoft.Json;
using TwoeterApi.Model.Repository;
using TwoeterApi.Model.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IUserFollowRepository, UserFollowRepository>();
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

var corsOrigin = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsOrigin, policy =>
    {
        policy.WithOrigins("http://twoeter-app.test")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
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

app.UseCors();

app.MapControllers();

app.Run();
