using DeliveryU.Api.Products.Domain.Models;
using DeliveryU.Api.Products.Domain.Repositories;
using DeliveryU.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace DeliveryU.Api.Products.Infrastructure.Adapters;

public class PostgresProductRepository : IProductRepository
{

    private readonly DatabaseManager _db;

    public PostgresProductRepository(DatabaseManager db)
    {
        _db = db;
    }
    public Product FetchProductById(string id)
    {
        return _db.Product.First(p => p.Id == id);
    }

    public ICollection<Product> FetchProductsByStore(string store)
    {
      return _db.Product.Include(p => p.Store).Where(p => p.Store.Id == store).ToList();
    }

    public ICollection<Product> SearchInListById(ICollection<string> ids)
    {
        return _db.Product.Where(p => ids.Contains(p.Id)).ToList();
    }
    public ICollection<Product> FetchProductByCategory (string idstore, string category)
    {
        return _db.Product.Include(p => p.Store).Where(p => p.Store.Id == idstore && p.Category == category).ToList();
    }

    public ICollection<string> FetchAllCategory(string idstore)
    {
       return _db.Product.Include(p => p.Store).Where(p => p.Store.Id == idstore)
       .Select(p => p.Category).Distinct().ToList();
    }

}