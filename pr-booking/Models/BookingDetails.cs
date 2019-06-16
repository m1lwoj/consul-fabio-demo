using System;

namespace PR.Booking.Models
{
    public class BookingDetails 
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public PaymentDetails Payment { get; set; }
        public PropertyDetails Property { get; set; }
    }
}
