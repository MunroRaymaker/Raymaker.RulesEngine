using FluentAssertions;
using NSubstitute;
using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.Model;
using Xunit;

namespace Raymaker.RulesEngine.UnitTests
{
    public class UserServiceTest
    {
        private readonly IUserService sut;
        private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

        public UserServiceTest()
        {
            sut = new UserService(this.userRepository);
        }

        [Theory]
        [InlineData("testtest.com")]
        [InlineData("testtest")]
        [InlineData("test@test")]
        public void Should_not_update_user_when_user_has_invalid_email(string email)
        {
            // Arrange
            var user = new User
            {
                MembershipType = MembershipType.VIP,
                Email = email
            };

            // Act
            var result = sut.UpdateUser(user);

            // Assert
            result.Should().BeFalse();
            this.userRepository.Received(0).UpdateUser(Arg.Any<User>());
        }

        [Theory]
        [InlineData("test@test.com","foo", true)]
        [InlineData("test@test.com","", false)]
        public void UpdateUser_should_save_user_when_parameters_are_valid(string email, string userName, bool isActive)
        {
            // Arrange
            var user = new User
            {
                Email = email,
                MembershipType = MembershipType.Basic,
                UserName = userName, 
                IsActive = isActive
            };
            this.userRepository.UpdateUser(Arg.Any<User>()).Returns(true);

            // Act
            var result = sut.UpdateUser(user);

            // Assert
            result.Should().BeTrue();
        }
    }
}
