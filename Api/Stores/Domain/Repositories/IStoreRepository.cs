using DeliveryU.Api.Stores.Domain.Models;

namespace DeliveryU.Api.Stores.Domain.Repositories;

public interface IStoreRepository
{
    ICollection<Store> FetchAll();
    Store? FetchStoreById(string storeId);
}