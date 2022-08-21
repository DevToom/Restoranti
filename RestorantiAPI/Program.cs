using Domain.Interface;
using Domain.Services;
using Infra.Repository;
using Infra.Repository.Generics;
using Infra.Repository.Generics.Interface;
using Infra.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(typeof(IRestorantiGeneric<>), typeof(RestorantiRepository<>));
builder.Services.AddSingleton<IRUser, RUser>();

#region Configure Repositories

builder.Services.AddSingleton(typeof(IRestorantiGeneric<>), typeof(RestorantiRepository<>));
builder.Services.AddSingleton<IRUser, RUser>();

#endregion

#region Configure Services

builder.Services.AddSingleton<IUserService, UserService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
