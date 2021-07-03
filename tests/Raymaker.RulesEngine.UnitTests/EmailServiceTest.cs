using FluentAssertions;
using Raymaker.RulesEngine.Application;
using Xunit;

namespace Raymaker.RulesEngine.UnitTests
{
    public class EmailServiceTest
    {
        private readonly IEmailService sut;

        public EmailServiceTest()
        {
            sut = new EmailService();
        }

        [Theory]
        [InlineData("testtest.com")]
        [InlineData("testtest")]
        [InlineData("test@test")]
        public void Should_not_send_email_when_user_has_invalid_email(string email)
        {
            // Arrange
            
            // Act
            var result = sut.SendEmail(email, "", "");

            // Assert
            result.Should().BeFalse();
        }
    }
}
