using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PR.Payments.Models;

namespace PR.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly string serviceAddress;

        public PaymentsController(IOptions<ServiceDiscoveryOptions> options)
        {
            this.serviceAddress = options.Value.ServiceAbsoluteUri;
        }

        [HttpGet("{bookingId}")]
        public ActionResult<PaymentDetails> Get([FromRoute] string bookingId)
        {
            return new PaymentDetails
            {
                BookingId = bookingId,
                Id = Guid.NewGuid().ToString(),
                Paid = new Random().NextDouble() < 0.5,
                Service = this.serviceAddress,
            };
        }

        [HttpGet()]
        public ActionResult<IEnumerable<PaymentDetails>> GetAll()
        {
            return new List<PaymentDetails>() {
                                new PaymentDetails{
                                    BookingId = Guid.NewGuid().ToString(),
                                    Id = Guid.NewGuid().ToString(),
                                    Paid = new Random().NextDouble() < 0.5,
                                    Service = this.serviceAddress,
                                },
                                new PaymentDetails{
                                    BookingId = Guid.NewGuid().ToString(),
                                    Id = Guid.NewGuid().ToString(),
                                    Paid = new Random().NextDouble() < 0.5,
                                    Service = this.serviceAddress,
                                },
                                new PaymentDetails{
                                    BookingId = Guid.NewGuid().ToString(),
                                    Id = Guid.NewGuid().ToString(),
                                    Paid = new Random().NextDouble() < 0.5,
                                    Service = this.serviceAddress,
                                },
                            };
        }
    }
}
