namespace Shop.Order
{
    public static class Extensions
    {
        public static OrderDto AsDto(this Entities.Order order)
        {
            return new OrderDto(order.Id, order.CustomerId, order.Quantity, order.Price, order.Status, order.Address,
                order.Product, order.CreatedAt, order.UpdatedAt);
        }
    }
}