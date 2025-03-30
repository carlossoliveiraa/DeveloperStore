using DeveloperStore.Sales.Application.Commands.CreateSale;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Repositories;
using Moq;

namespace DeveloperStore.Sales.Tests.Commands.CreateSale
{
    public class CreateSaleCommandHandlerTests
    {
        [Fact]
        public async Task Should_Create_Sale_And_Return_Id()
        {
            //// Arrange
            var fakeRepository = new Mock<ISaleRepository>();

            var handler = new CreateSaleCommandHandler(fakeRepository.Object);

            var command = new CreateSaleCommand(
                SaleNumber: "V-1001",
                CustomerId: Guid.NewGuid(),
                CustomerName: "Carlos Oliveira",
                BranchId: Guid.NewGuid(),
                BranchName: "São Paulo",
                Items: new List<CreateSaleItemDto>
                {
                new(Guid.NewGuid(), "Mouse", 5, 100m),
                new(Guid.NewGuid(), "Teclado", 3, 150m)
                }
            );

            //// Act
            var result = await handler.Handle(command, CancellationToken.None);

            //// Assert
            Assert.NotEqual(Guid.Empty, result);
            fakeRepository.Verify(x => x.AddAsync(It.IsAny<Sale>()), Times.Once);
        }
    }
}
