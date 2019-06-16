using System;

namespace PR.Booking.Models
{
    public class PaymentDetails 
    {
        public string Id { get; set; }
        public string BookingId { get; set; }
        public bool Paid { get; set; }
        public string Notes { get; set; }
        public string Service { get; set; }
    }
}
