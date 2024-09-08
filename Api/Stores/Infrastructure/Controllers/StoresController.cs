using DeliveryU.Api.Stores.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DeliveryU.Api.Stores.Infrastructure.Controllers;

[Route("api/stores")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [Authorize]
    [HttpGet("all")]
    public IActionResult FetchAll()
    {
        var stores = _storeService.FetchAll();
        return Ok(stores);
    }

    [HttpGet("{storeId}")]
    public IActionResult FetchStoreById (string storeId)
    {
        var stores = _storeService.FetchStoreById(storeId);

        if (stores == null)
        {
            return Forbid();
        }
        return Ok(stores);
    }

}