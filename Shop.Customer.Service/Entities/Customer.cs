using System;
using Shop.Common;
using Shop.Customer.Service.Model;

namespace Shop.Customer.Service.Entities
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}