namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class EmailNotificationBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "EmailNotification";

        // If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
        public (bool isSatisfied, string message) Process(Order order)
        {
            var product = order.Product as Membership;

            if (product == null)
            {
                return (false, $"{NameRequirement}: No action");
            }

            (order.Product as Membership).EmailsSent += 1;
            return (true, $"{NameRequirement}: Email was sent");
        }
    }
}
