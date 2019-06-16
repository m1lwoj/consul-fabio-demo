using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PR.Booking.Models;
using PR.Booking.Services;

namespace PR.Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;
        private readonly IAccommodationService _accommodationService;

        public BookingsController(IPaymentsService paymentsService, IAccommodationService accommodationService)
        {
            _paymentsService = paymentsService;
            _accommodationService = accommodationService;
        }

        [HttpGet]
        [Route("{bookingId}")]
        public async Task<BookingDetails> Get(string bookingId)
        {
            var payment = await _paymentsService.GetAsync(bookingId);
            var accommodation = await _accommodationService.GetAsync(Guid.NewGuid().ToString());
            
            return new BookingDetails{
                Description = $"Booking number {bookingId}",
                Payment = payment,
                Id = bookingId,
                Property = accommodation
            };
        }

        
        [HttpGet]
        public async Task<ActionResult<List<BookingDetails>>> GetAll()
        {
            var payments = await _paymentsService.GetAll();
            var properties = await _accommodationService.GetAll();

            return new List<BookingDetails>() {
                new BookingDetails{
                    Description = $"Booking number {Guid.NewGuid().ToString()}",
                    Id = Guid.NewGuid().ToString(),
                    Payment = payments.ElementAt(0),
                    Property = properties.ElementAt(0)
                },
                 new BookingDetails{
                    Description = $"Booking number {Guid.NewGuid().ToString()}",
                    Id = Guid.NewGuid().ToString(),
                    Payment = payments.ElementAt(1),
                    Property = properties.ElementAt(1)
                },
                new BookingDetails{
                    Description = $"Booking number {Guid.NewGuid().ToString()}",
                    Id = Guid.NewGuid().ToString(),
                    Payment = payments.ElementAt(2),
                    Property = properties.ElementAt(2)
                },
            };
        }
    }
}
