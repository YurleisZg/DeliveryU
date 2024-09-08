using DeliveryU.Api.Stores.Domain.Models;
using DeliveryU.Api.Stores.Domain.Repositories;
using DeliveryU.Api.Stores.Domain.Services;

namespace DeliveryU.Api.Stores.Application;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public ICollection<Store> FetchAll()
    {
        return _storeRepository.FetchAll();
    }

    public Store? FetchStoreById(string storeId)
    {
        return _storeRepository.FetchStoreById(storeId);
    }
}