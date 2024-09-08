using DeliveryU.Api.Products.Domain.Models;

namespace DeliveryU.Api.Products.Domain.Services;

public interface IProductService
{
     ICollection<Product> FetchProductsByStore(string store);
     ICollection<Product> SearchInListById(ICollection<string> ids);
     Product FetchProductById(string id);
     ICollection<Product> FetchProductByCategory(string idstore, string category);
     ICollection<string> FetchAllCategory(string idstore);
}