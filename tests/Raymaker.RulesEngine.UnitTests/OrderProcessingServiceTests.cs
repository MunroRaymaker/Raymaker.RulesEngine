using FluentAssertions;
using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using Xunit;

namespace Raymaker.RulesEngine.UnitTests
{
    public class OrderProcessingServiceTests
    {
        private readonly OrderProcessingService sut;

        public OrderProcessingServiceTests()
        {
            var providers = new BusinessRuleProviderFactory();
            sut = new OrderProcessingService(providers.GetProviders());
        }

        [Fact]
        public void Should_process_packing_slip_when_order_has_book()
        {
            // Arrange
            var order = new Order{ Product = new Book() };

            // Act
            sut.Process(ref order);

            // Assert
            order.PackingSlip.Should().Contain("shipping");
        }
    }
}
