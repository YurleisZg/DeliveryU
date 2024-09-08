using DeliveryU.Api.People.Domain.Services;
using DeliveryU.Api.People.Infrastructure.Dtos;
using DeliveryU.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryU.Api.People.Infrastructure.Controllers;

[Route("api/person")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly INotificationService _notificationService;

    public PersonController(IPersonService personService, INotificationService notificationService)
    {
        _personService = personService;
        _notificationService = notificationService;
    }

    [Authorize]
    [HttpGet("info")]
    public IActionResult CheckContactInformation()
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if(id == null) {
            return Unauthorized();
        }

        var info = _personService.FetchPersonById(id);
        
        if (info == null)
        {
            return NotFound();
        }

        var contactInfo = new PersonRequestDto 
        {
            Name = info.Name,
            LastName = info.LastName,
            PhoneNumber = info.PhoneNumber,
        };

        return Ok(contactInfo);
    }

    [Authorize]
    [HttpPut("upgrade/delivery")]
    public async Task<IActionResult> UpgradePersonToDeliveryAsync() 
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if(id == null) {
            return Unauthorized();
        }

        var updated = _personService.UpdateToDelivery(id);

        if(!updated) {
            return Forbid();
        }

       await _notificationService.SubscribeToTopicAsync(id, "delivery");
        return Ok();
    }
    
    [Authorize]
    [HttpPut("outdated/delivery")]
    public IActionResult OutdatedDelivery() 
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if(id == null) 
        {
            return Unauthorized();
        }
        
        var outdated = _personService.OutdatedDelivery(id);

        if(!outdated) {
            return Forbid();
        }

        return Ok();
   }
}