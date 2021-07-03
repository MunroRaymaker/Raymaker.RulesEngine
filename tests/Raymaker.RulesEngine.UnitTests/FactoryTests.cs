using FluentAssertions;
using NSubstitute;
using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using Xunit;

namespace Raymaker.RulesEngine.UnitTests
{
    public class FactoryTests
    {
        private readonly BusinessRuleProviderFactory factory;
        private readonly UserService userService = Substitute.For<UserService>();
        private readonly IEmailService emailService = Substitute.For<IEmailService>();

        public FactoryTests()
        {
            factory = new BusinessRuleProviderFactory(userService, emailService);
        }

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
            var (isSatisfied, message) = provider.Process(order);

            // Assert
            isSatisfied.Should().BeTrue();
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
            var (isSatisfied, message) = provider.Process(order);

            // Assert
            isSatisfied.Should().BeTrue();
        }
    }
}
