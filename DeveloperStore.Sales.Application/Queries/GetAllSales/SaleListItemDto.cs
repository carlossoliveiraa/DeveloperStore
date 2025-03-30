namespace DeveloperStore.Sales.Application.Queries.GetAllSales
{
    public sealed class SaleListItemDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public bool IsCancelled { get; set; }
    }
}
