namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class EmailNotificationBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public EmailNotificationBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
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

            var user = userService.GetUser(product.MemberName);
            user.EmailsSent += 1;
            user.Email = product.MemberEmail;

            // Send email
            var result = this.userService.UpdateUser(user);

            return (result, $"{NameRequirement}: Email was sent");
        }
    }
}
