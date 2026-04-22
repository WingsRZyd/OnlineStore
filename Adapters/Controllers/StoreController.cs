using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.UseCases;
using OnlineStore.Models;

namespace OnlineStore.Adapters.Controllers;

[ApiController]
[Route("api")]
public class StoreController : ControllerBase
{
    private readonly GetProductsUseCase _getProductsUseCase;
    private readonly AddItemToCartUseCase _addItemToCartUseCase;
    private readonly GetCartUseCase _getCartUseCase;
    private readonly CreateOrderUseCase _createOrderUseCase;

    public StoreController(
        GetProductsUseCase getProductsUseCase,
        AddItemToCartUseCase addItemToCartUseCase,
        GetCartUseCase getCartUseCase,
        CreateOrderUseCase createOrderUseCase)
    {
        _getProductsUseCase = getProductsUseCase;
        _addItemToCartUseCase = addItemToCartUseCase;
        _getCartUseCase = getCartUseCase;
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpGet("products")]
    public async Task<ActionResult<List<ProductResponse>>> GetProducts(
        string? category = null, 
        decimal? minPrice = null, 
        decimal? maxPrice = null)
    {
        var products = await _getProductsUseCase.ExecuteAsync(category, minPrice, maxPrice);
        
        var response = products.Select(p => new ProductResponse
        {
            Id = p.Id.Value,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price.Amount,
            Stock = p.Stock,
            Category = p.Category
        }).ToList();

        return Ok(response);
    }

    [HttpPost("cart/items")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        await _addItemToCartUseCase.ExecuteAsync(request.ProductId, request.Quantity);
        return Ok(new { message = "Товар добавлен в корзину" });
    }

    [HttpGet("cart")]
    public async Task<ActionResult<CartResponse>> GetCart()
    {
        var cart = await _getCartUseCase.ExecuteAsync();

        var response = new CartResponse
        {
            Items = cart.Items.Select(i => new CartItemResponse
            {
                ProductId = i.ProductId.Value,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                Price = i.PricePerUnit.Amount
            }).ToList(),
            TotalPrice = cart.GetTotal().Amount
        };

        return Ok(response);
    }

    [HttpPost("orders")]
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = await _createOrderUseCase.ExecuteAsync(request.DeliveryAddress, request.PaymentMethod);
        
        return Ok(new
        {
            message = "Заказ успешно оформлен",
            orderId = order.Id.Value,
            totalAmount = order.TotalAmount.Amount,
            status = order.Status.ToString()
        });
    }
}