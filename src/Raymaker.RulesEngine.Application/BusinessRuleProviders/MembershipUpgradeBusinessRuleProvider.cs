namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class MembershipUpgradeBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "MembershipUpgrade";

        // If the payment is an upgrade to a membership, apply the upgrade
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(MembershipProduct) &&
                (order.Product as MembershipProduct).MembershipType == "Upgrade")
            {
                // Process upgrade
                (order.Product as MembershipProduct).MembershipType = "VIP";
                return true;
            }

            return false;
        }
    }
}
