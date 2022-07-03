
namespace Shop.Customer.Service
{
    public static class Extensions
    {
        public static CustomerDto AsDto(this Entities.Customer customer)
        {
            return new CustomerDto(customer.Id, customer.Name, customer.Email, customer.Address, customer.CreatedDate);
        }
    }
}