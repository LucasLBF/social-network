using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Services.Abstractions;
using SocialNetwork.API.Services.Implementations;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Repositories.Abstractions;
using SocialNetwork.Data.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<IUserService, UserService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SocialNetworkContext>(opt =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (connectionString == null)
        throw new ArgumentNullException(nameof(connectionString), "No connection string was found");

    opt
    .UseLazyLoadingProxies()
    .UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
