using DeliveryU.Api.Products.Domain.Models;
using DeliveryU.Api.Products.Domain.Repositories;
using DeliveryU.Api.Products.Domain.Services;

namespace DeliveryU.Api.Products.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Product FetchProductById(string id)
    {
        return _productRepository.FetchProductById(id);
    }

    public ICollection<Product> FetchProductsByStore(string store)
    {
        return _productRepository.FetchProductsByStore(store);  
    }

    public ICollection<Product> SearchInListById(ICollection<string> ids)
    {
        return _productRepository.SearchInListById(ids);
    }

    public ICollection<Product> FetchProductByCategory(string idstore, string category)
    {
        return _productRepository.FetchProductByCategory(idstore, category);
    }

    public ICollection<string> FetchAllCategory(string idstore)
    {
        return _productRepository.FetchAllCategory(idstore);
    }
}