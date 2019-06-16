using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PR.Accommodation.Models;

namespace PR.Accommodation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly string _serviceAddress;

        public AccommodationController(IOptions<ServiceDiscoveryOptions> options)
        {
            this._serviceAddress = options.Value.ServiceAbsoluteUri;
        }

        [HttpGet]
        [Route("{propertyId}")]
        public async Task<PropertyDetails> Get(string propertyId)
        {
            return new PropertyDetails
            {
                Description = @"Boasting barbecue facilities, ANTICA DIMORA DELL'ETNA provides accommodation in Puntalazzo with free WiFi and sea views. Guests staying at this holiday home have access to a fully equipped kitchen.
                            The holiday home includes 1 bedroom, a living room, and 1 bathroom with free toiletries and a bidet.
                            The holiday home offers a terrace. Guests can also relax in the garden.",
                Location = "37.39793, 14.658782",
                Id = propertyId,
                Service = _serviceAddress
            };
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyDetails>>> GetAll()
        {
            return new List<PropertyDetails>() {
                new PropertyDetails{
                    Description = @"
                            Boasting barbecue facilities, ANTICA DIMORA DELL'ETNA provides accommodation in Puntalazzo with free WiFi and sea views. Guests staying at this holiday home have access to a fully equipped kitchen.
                            The holiday home includes 1 bedroom, a living room, and 1 bathroom with free toiletries and a bidet.
                            The holiday home offers a terrace. Guests can also relax in the garden.",
                    Id = Guid.NewGuid().ToString(),
                    Location = "37.39793, 14.658782",
                    Service = _serviceAddress
                },
                 new PropertyDetails{
                    Description = $"Located in Lido Signorino, Anastasia Villas-B&B offers accommodation with free WiFi and flat-screen TV, as well as a private beach area. Some units have a terrace and/or a balcony with pool views.",
                    Id = Guid.NewGuid().ToString(),
                    Location = "37.45218, 14.757732",
                    Service = _serviceAddress
                },
                new PropertyDetails{
                    Description = $"Anita Open Space is located in Ognina and offers an outdoor swimming pool, a garden and a terrace. The air-conditioned accommodation is 19 km from Belvedere.",
                    Id = Guid.NewGuid().ToString(),
                    Location = "37.39128, 14.123572",
                    Service = _serviceAddress
                },
            };
        }
    }
}
