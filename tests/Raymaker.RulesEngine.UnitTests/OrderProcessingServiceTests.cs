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

        /// <summary>
        /// Rule: If the payment is for a physical product, 
        /// generate a packing slip for shipping.
        /// </summary>
        [Fact]
        public void Should_process_packing_slip_when_order_has_video()
        {
            // Arrange
            var order = new Order{ Product = new Video() };

            // Act
            sut.Process(ref order);

            // Assert
            order.PackingSlip.Should().Contain("shipping");
        }

        /// <summary>
        /// Rule: If the payment is for a book, create a duplicate packing slip 
        /// for the royalty department.
        /// </summary>
        [Fact]
        public void Should_process_packing_slip_when_order_has_book()
        {
            // Arrange
            var order = new Order{ Product = new Book() };

            // Act
            sut.Process(ref order);

            // Assert
            order.PackingSlip.Should().Contain("royalty");
            order.PackingSlip.Should().Contain("shipping");
        }

        /// <summary>
        /// Rule: If the payment is for a membership, activate that membership.
        /// </summary>
        [Fact]
        public void Should_activate_membership_when_order_has_membership()
        {
            // Arrange
            var order = new Order{ Product = new MembershipProduct() };

            // Act
            sut.Process(ref order);

            // Assert
            (order.Product as MembershipProduct).IsActive.Should().BeTrue();
        }

        /// <summary>
        /// Rule: If the payment is an upgrade to a membership, apply the upgrade.
        /// </summary>
        [Fact]
        public void Should_upgrade_membership_when_order_is_membershipupgrade()
        {
            // Arrange
            var order = new Order{ Product = new MembershipProduct{ MembershipType = "Upgrade" } };

            // Act
            sut.Process(ref order);

            // Assert
            (order.Product as MembershipProduct).MembershipType.Should().Be("Upgrade");
        }

        /// <summary>
        /// Rule: If the payment is for a membership or upgrade, 
        /// e-mail the owner and inform them of the activation/upgrade.
        /// </summary>
        [Fact]
        public void Should_send_email_when_order_has_membershipupgrade()
        {
            // Arrange
            var order = new Order{ Product = new MembershipProduct{ MembershipType = "Upgrade", MemberEmail = "test@test.com" } };

            // Act
            sut.Process(ref order);

            // Assert
            (order.Product as MembershipProduct).EmailsSent.Should().BeGreaterThan(0);
        }

        /// <summary>
        /// Rule: If the payment is for the video “Learning to Ski,” 
        /// add a free “First Aid” video to the packing slip 
        /// (the result of a court decision in 1997).
        /// </summary>
        [Fact]
        public void Should_process_extra_item_when_order_has_skiing_video()
        {
            // Arrange
            var order = new Order{ Product = new Video() };

            // Act
            sut.Process(ref order);

            // Assert
            order.PackingSlip.Should().Contain("First Aid");
        }

        /// <summary>
        /// Rule: If the payment is for a physical product or a book, 
        /// generate a commission payment to the agent.
        /// </summary>
        [Fact]
        public void Should_process_commission_payment_when_order_has_book()
        {
            // Arrange
            var order = new Order{ Product = new Book() };

            // Act
            sut.Process(ref order);

            // Assert
            order.Payment.Should().NotBeNull();
        }
    }
}
