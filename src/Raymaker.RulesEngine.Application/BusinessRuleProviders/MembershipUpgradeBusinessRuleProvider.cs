using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class MembershipUpgradeBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public MembershipUpgradeBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement => "MembershipUpgrade";

        // If the payment is an upgrade to a membership, apply the upgrade
        public (bool isSatisfied, string message) Process(Order order)
        {
            if (order.Product.GetType() == typeof(MembershipUpgrade))
            {
                // Process upgrade
                var product = (MembershipUpgrade)order.Product;
                var user = userService.GetUser(product.MemberName);
                user.MembershipType = product.MembershipType;

                var result = userService.UpdateUser(user);   

                return (result, $"{NameRequirement}: Upgraded to VIP");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
