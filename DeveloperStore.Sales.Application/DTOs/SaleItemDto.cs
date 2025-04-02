﻿namespace DeveloperStore.Sales.Application.DTOs
{
    public class SaleItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
