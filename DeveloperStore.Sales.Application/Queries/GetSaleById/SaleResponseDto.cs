namespace DeveloperStore.Sales.Application.Queries.GetSaleById
{
    public sealed class SaleResponseDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = default!;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = default!;

        public bool IsCancelled { get; set; }
        public decimal TotalAmount { get; set; }

        public List<SaleItemResponseDto> Items { get; set; } = new();
    }
}
