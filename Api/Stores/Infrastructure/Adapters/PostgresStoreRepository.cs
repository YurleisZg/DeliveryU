using DeliveryU.Api.Stores.Domain.Models;
using DeliveryU.Api.Stores.Domain.Repositories;
using DeliveryU.Persistence.Database;

namespace DeliveryU.Api.Stores.Infrastructure.Adapters;

public class PostgresStoreRepository : IStoreRepository
{
    private readonly DatabaseManager _db;

    public PostgresStoreRepository(DatabaseManager db)
    {
        _db = db;
    }
    public ICollection<Store> FetchAll()
    {
        return _db.Store.ToList();
    }

    public Store? FetchStoreById(string storeId)
    {
        return _db.Store.FirstOrDefault(o => o.Id == storeId);
    }
}