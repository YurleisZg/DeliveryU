using DeliveryU.Api.Stores.Domain.Models;

namespace DeliveryU.Api.Stores.Domain.Services;

public interface IStoreService
{
    ICollection<Store> FetchAll();
    Store? FetchStoreById(string storeId);
}