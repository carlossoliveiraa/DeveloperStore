﻿namespace DeveloperStore.Sales.Application.DTOs.Inputs
{
    public class SaleInputDto
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<SaleItemInputDto> Items { get; set; } = new();
    }
}
