using FluentAssertions;
using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using Xunit;

namespace Raymaker.RulesEngine.UnitTests
{
    public class FactoryTests
    {
        private readonly BusinessRuleProviderFactory factory = new BusinessRuleProviderFactory();

        [Fact]
        public void when_order_has_book_processes_rule()
        {
            // Arrange
            var provider = factory.GetProviderByClientName("PhysicalProduct");
            var order = new Order
            {
                Product = new Book()
            };

            // Act
            var result = provider.Process(ref order);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void when_order_has_video_processes_rule()
        {
            // Arrange
            var provider = factory.GetProviderByClientName("PhysicalProduct");
            var order = new Order
            {
                Product = new Video()
            };

            // Act
            var result = provider.Process(ref order);

            // Assert
            result.Should().BeTrue();
        }
    }
}
