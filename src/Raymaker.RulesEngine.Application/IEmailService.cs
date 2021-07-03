namespace Raymaker.RulesEngine.Application
{
    public interface IEmailService
    {
        public bool SendEmail(string recipient, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly UserValidator userValidator;

        public EmailService()
        {
            this.userValidator = new UserValidator();
        }

        public bool SendEmail(string recipient, string subject, string body)
        {
            return userValidator.HasValidEmail(recipient);
        }
    }
}