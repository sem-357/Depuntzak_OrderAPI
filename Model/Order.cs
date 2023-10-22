using System;
using System.Collections.Generic;

namespace DePuntzak_OrderAPI.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }
        public int? Subtotal { get; set; }
        public List<OrderItem> OrderItems { get; internal set; }
    }
}
