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

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        public decimal TotalAmount { get; private set; }

        public Sale(
            string saleNumber,
            DateTime saleDate,
            Guid customerId,
            string customerName,
            Guid branchId,
            string branchName)
        {
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new BusinessRuleValidationException("Sale number is required.");

            if (customerId == Guid.Empty)
                throw new BusinessRuleValidationException("Customer ID is required.");

            if (string.IsNullOrWhiteSpace(customerName))
                throw new BusinessRuleValidationException("Customer name is required.");

            if (branchId == Guid.Empty)
                throw new BusinessRuleValidationException("Branch ID is required.");

            if (string.IsNullOrWhiteSpace(branchName))
                throw new BusinessRuleValidationException("Branch name is required.");

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
            if (IsCancelled)
                throw new BusinessRuleValidationException("Cannot add items to a cancelled sale.");

            var item = new SaleItem(productId, productName, quantity, unitPrice);
            _items.Add(item);
            TotalAmount += item.Total;
        }

        public void Cancel()
        {
            if (IsCancelled)
                throw new BusinessRuleValidationException("Sale is already cancelled.");

            IsCancelled = true;
        }

        public void ClearItems()
        {
            if (IsCancelled)
                throw new BusinessRuleValidationException("Cannot clear items from a cancelled sale.");

            _items.Clear();
            TotalAmount = 0m;
        }
    }
}
