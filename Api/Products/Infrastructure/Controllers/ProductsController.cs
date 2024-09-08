using DeliveryU.Api.Products.Domain.Services;
using DeliveryU.Api.Products.Infrastructure.Dtos;
using DeliveryU.Shared.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryU.Api.Products.Infrastructure.Controllers;

[Route("api/store/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize]
    [HttpGet("all")]
    public IActionResult FetchProductByStore(string store)
    {
        var products = _productService.FetchProductsByStore(store);

        if (!products.Any())
        {
            throw new ProductNotFoundException();
        }
        
        var response = new ProductRequestDto
        {
            Products = products
        };

        return Ok(products);
    }

    [Authorize]
    [HttpGet("category")]
    public IActionResult FetchProductByCategory (string idstore, string category)
    {
        var products = _productService.FetchProductByCategory(idstore, category);

        if (!products.Any())
        {
            return NotFound ("Categoria no disponible");
        }
        
        var response = new ProductRequestDto
        {
            Products = products
        };

        return Ok(response);
    }

    [Authorize]
    [HttpGet("all category")]
    public ActionResult FetchAllCategory (string idstore)
    {
        var categories = _productService.FetchAllCategory(idstore);
       
        if (categories == null)
        {
            return Forbid();
        }

        return Ok(categories);
    }
}