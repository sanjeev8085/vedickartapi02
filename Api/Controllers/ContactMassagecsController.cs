using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VadicKart.Entity.Presentation.Dto.ContactMessage;
using VadicKart.Entity.Presentation.Dto.LogIn;
using VadicKart.Entity.Presentation.RequestFeatures;
using Vedickart.Services;
using VedicKart.Service.Contract;
using VedicKart.Service.Contract.IContactMassagecs;
namespace vedickartApi.Api.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class ContactMessageController(IServiceManager service) : Controller
    {
        private readonly IServiceManager _service = service;
        [HttpGet]
        public async Task<IActionResult> GetAllContactMessage([FromQuery] RequestParameters? requestParameters)
        {
          
            var (contactMassagecs, metaData) = await _service.ContactMessageService.GetAllContactMassagecsAsync(requestParameters);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));
            return Ok(new { contactMassagecs , metaData});
        }

        

    

        [HttpPost]
        public async Task<IActionResult> CreateContactMessage( [FromBody] ContactMassagecsCreationDto contactMassagecs)
        {
            if (contactMassagecs == null || contactMassagecs!.Email == "string" || contactMassagecs!.Message == "string")
            {
                return BadRequest("Enter a valid email or password");
            }
            var addresses = await _service.ContactMessageService.CreateContactMassagecs(contactMassagecs);
            return Ok(addresses);
        }

      

    }
}
