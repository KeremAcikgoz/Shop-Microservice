using System;
using Shop.Common;
using Shop.Customer.Service.Model;
using Shop.Order.Model;

namespace Shop.Order.Entities
{
    public class Order : IEntity
    {
        public Guid Id { get; set; }
        
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public Address Address { get; set; }
        public Product Product { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}