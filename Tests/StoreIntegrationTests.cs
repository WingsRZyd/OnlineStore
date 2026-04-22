using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using OnlineStore.Models;
using Xunit;

namespace OnlineStore.Tests;

public class StoreIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StoreIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
        });
    }

    [Fact]
    public async Task GetProducts_ShouldReturnOkAndProducts()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/products");
        Assert.True(response.IsSuccessStatusCode, $"Expected OK, got {response.StatusCode}");
        
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        Assert.NotNull(products);
        Assert.True(products.Count > 0);
    }

    [Fact]
    public async Task AddToCart_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var productsResponse = await client.GetAsync("/api/products");
        var products = await productsResponse.Content.ReadFromJsonAsync<List<ProductResponse>>();
        var firstProduct = products!.First();

        var request = new AddToCartRequest 
        { 
            ProductId = firstProduct.Id, 
            Quantity = 1 
        };

        var response = await client.PostAsJsonAsync("/api/cart/items", request);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Assert.True(false, $"AddToCart failed with status {response.StatusCode}. Error: {error}");
        }

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task FullWorkflow_ShouldWork()
    {
        var client = _factory.CreateClient();

        var productsResponse = await client.GetAsync("/api/products");
        var products = await productsResponse.Content.ReadFromJsonAsync<List<ProductResponse>>();
        var product = products!.First();

        await client.PostAsJsonAsync("/api/cart/items", new AddToCartRequest 
        { 
            ProductId = product.Id, 
            Quantity = 2 
        });

        var orderRequest = new CreateOrderRequest 
        { 
            DeliveryAddress = "Санкт-Петербург, ул. Пушкина 10",
            PaymentMethod = "Card"
        };

        var response = await client.PostAsJsonAsync("/api/orders", orderRequest);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Assert.True(false, $"FullWorkflow failed with status {response.StatusCode}. Error: {error}");
        }

        Assert.True(response.IsSuccessStatusCode);
    }
}