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
                // TODO This should be set on a user class.
                (order.Product as Membership).MembershipType = MembershipType.VIP;
                return (true, $"{NameRequirement}: Upgraded to VIP");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
