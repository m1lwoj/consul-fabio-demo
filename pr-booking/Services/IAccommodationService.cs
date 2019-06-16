using System.Collections.Generic;
using System.Threading.Tasks;
using PR.Booking.Models;
using RestEase;

namespace PR.Booking.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IAccommodationService
    {
        [AllowAnyStatusCode]
        [Get("api/accommodation/{propertyId}")]
        Task<PropertyDetails> GetAsync([Path] string propertyId);

        [AllowAnyStatusCode]
        [Get("api/accommodation")]
        Task<IEnumerable<PropertyDetails>> GetAll();
    }
}