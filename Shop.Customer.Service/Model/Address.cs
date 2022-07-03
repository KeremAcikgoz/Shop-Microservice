using System.ComponentModel.DataAnnotations;

namespace Shop.Customer.Service.Model
{
    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        [Range(1, 10000)]
        public int CityCode { get; set; }
    }
}