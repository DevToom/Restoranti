using Domain.Interface;
using Domain.Services;
using Infra.Repository;
using Infra.Repository.Generics;
using Infra.Repository.Generics.Interface;
using Infra.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure Repositories
ConfigureRepositories();
#endregion

#region Configure Services
ConfigureServices();
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

void ConfigureRepositories()
{
    builder.Services.AddSingleton(typeof(IRestorantiGeneric<>), typeof(RestorantiRepository<>));
    builder.Services.AddSingleton<IRUserInternal, RUserInternal>();
    builder.Services.AddSingleton<IROrder, ROrder>();
    builder.Services.AddSingleton<IRCategory, RCategory>();
}

void ConfigureServices()
{
    builder.Services.AddSingleton<IUserInternalService, UserInternalService>();
    builder.Services.AddSingleton<IOrderService, OrderService>();
    builder.Services.AddSingleton<ICategoryService, CategoryService>();
}