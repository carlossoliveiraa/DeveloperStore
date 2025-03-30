using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.Domain.Entities
{
    public sealed class Sale : BaseEntity
    {
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }

        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }

        public bool IsCancelled { get; private set; }

        public List<SaleItem> Items { get; private set; } = new();

        public decimal TotalAmount => Items.Sum(item => item.Total);

        public Sale(string saleNumber, DateTime saleDate, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new BusinessRuleValidationException("Sale number is required.");

            if (customerId == Guid.Empty)
                throw new BusinessRuleValidationException("Customer ID is required.");

            if (branchId == Guid.Empty)
                throw new BusinessRuleValidationException("Branch ID is required.");

            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
            IsCancelled = false;
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(productId, productName, quantity, unitPrice);
            Items.Add(item);
        }

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}