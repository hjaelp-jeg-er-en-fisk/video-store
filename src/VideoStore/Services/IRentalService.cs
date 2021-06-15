using VideoStore.Models;

namespace VideoStore.Services
{
    public interface IRentalService
    {
        string GetCustomerRentalRecordStatement(Customer customer);
    }
}