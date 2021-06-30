namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class EmailNotificationBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "EmailNotification";

        // If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
        public bool Process(Order order)
        {
            var product = order.Product as MembershipProduct;

            if (product == null)
            {
                return false;
            }

            if (product.MembershipType == "Basic" ||
                product.MembershipType == "Upgrade" ||
                product.MembershipType == "VIP")
            {
                (order.Product as MembershipProduct).EmailsSent += 1;
                return true;
            }

            return false;
        }
    }
}
