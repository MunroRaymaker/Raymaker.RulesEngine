using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class EmailNotificationBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public EmailNotificationBusinessRuleProvider(IUserService userService, IEmailService emailService)
        {
            this.userService = userService;
            this.emailService = emailService;
        }

        public string NameRequirement => "EmailNotification";

        // If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
        public (bool isSatisfied, string message) Process(Order order)
        {
            var product = order.Product as Membership;

            if (product == null)
            {
                return (false, $"{NameRequirement}: No action");
            }

            var result = this.emailService.SendEmail(product.MemberEmail, "Hello", "Your account was updated.");

            if (result == true)
            {
                var user = userService.GetUser(product.MemberName);
                user.EmailsSent += 1;
                user.Email = product.MemberEmail;
                result = this.userService.UpdateUser(user);
            }

            return (result, $"{NameRequirement}: Email was sent");
        }
    }
}
