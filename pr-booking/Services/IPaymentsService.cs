using System.Collections.Generic;
using System.Threading.Tasks;
using PR.Booking.Models;
using RestEase;

namespace PR.Booking.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IPaymentsService
    {
        [AllowAnyStatusCode]
        [Get("api/payments/{bookingId}")]
        Task<PaymentDetails> GetAsync([Path] string bookingId); 

        [AllowAnyStatusCode]
        [Get("api/payments")]
        Task<IEnumerable<PaymentDetails>> GetAll(); 
    }
}