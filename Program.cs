using OnlineStore.Application.Interfaces;
using OnlineStore.Application.UseCases;
using OnlineStore.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GetProductsUseCase>();
builder.Services.AddScoped<AddItemToCartUseCase>();
builder.Services.AddScoped<GetCartUseCase>();
builder.Services.AddScoped<CreateOrderUseCase>();

builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<ICartRepository, InMemoryCartRepository>();
builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();     // ← Это важно для тестов
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }