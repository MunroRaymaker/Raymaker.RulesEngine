using FluentAssertions;
using NSubstitute;
using Raymaker.RulesEngine.Application;
using Xunit;

namespace Raymaker.RulesEngine.UnitTests
{
    public class OrderProcessingServiceTests
    {
        private readonly OrderProcessingService sut;
        private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

        public OrderProcessingServiceTests()
        {
            var userService = new UserService(this.userRepository);
            sut = new OrderProcessingService(userService);
        }

        /// <summary>
        /// Rule: If the payment is for a physical product, 
        /// generate a packing slip for shipping.
        /// </summary>
        [Fact]
        public void Should_process_packing_slip_when_order_has_video()
        {
            // Arrange
            var order = new Order { Product = new Video() };

            // Act
            sut.Process(order);

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
            var order = new Order { Product = new Book() };

            // Act
            sut.Process(order);

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
            var order = new Order { Product = new Membership { MemberName = "foo", MemberEmail = "test@test.com", MembershipType = MembershipType.Basic } };
            this.userRepository.GetUser("foo").Returns(new User
            { MembershipType = MembershipType.Basic, Email = "test@test.com" });

            // Act
            sut.Process(order);

            // Assert
            (order.Product as Membership).IsActive.Should().BeTrue();
            this.userRepository.Received(2).UpdateUser(Arg.Is<User>(user =>
                user.MembershipType == MembershipType.Basic &&
                user.IsActive == true));
        }

        /// <summary>
        /// Rule: If the payment is an upgrade to a membership, apply the upgrade.
        /// </summary>
        [Fact]
        public void Should_upgrade_membership_when_order_is_membershipupgrade()
        {
            // Arrange
            var order = new Order { Product = new MembershipUpgrade() { MemberName = "foo", MembershipType = MembershipType.VIP, MemberEmail = "test@test.com" } };
            this.userRepository.GetUser("foo").Returns(new User
            { MembershipType = MembershipType.Basic, Email = "test@test.com" });

            // Act
            sut.Process(order);

            // Assert
            this.userRepository.Received(2).UpdateUser(Arg.Is<User>(user =>
                user.MembershipType == MembershipType.VIP));
        }

        /// <summary>
        /// Rule: If the payment is for a membership or upgrade, 
        /// e-mail the owner and inform them of the activation/upgrade.
        /// </summary>
        [Fact]
        public void Should_send_email_when_order_has_membershipupgrade()
        {
            // Arrange
            var order = new Order { Product = new MembershipUpgrade { MemberEmail = "test@test.com", MemberName = "foo" } };
            this.userRepository.GetUser("foo").Returns(new User
            { MembershipType = MembershipType.VIP, Email = "test@test.com" });

            // Act
            sut.Process(order);

            // Assert
            this.userRepository.Received(2).UpdateUser(Arg.Is<User>(user =>
               user.EmailsSent > 0));
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
            var order = new Order { Product = new Video { Name = "Learning to Ski" } };

            // Act
            sut.Process(order);

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
            var order = new Order { Product = new Book(), SoldBy = "Agent" };
            this.userRepository.UpdateAgent(Arg.Any<Agent>()).Returns(true);

            // Act
            sut.Process(order);

            // Assert
            this.userRepository.Received(1).UpdateAgent(Arg.Is<Agent>(agent => 
                agent.Name == "Agent" && agent.Commission == 10));
        }
    }
}
