namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class MembershipUpgradeBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "MembershipUpgrade";

        // If the payment is for a membership, upgrade that membership.
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(MembershipProduct) &&
                (order.Product as MembershipProduct).MembershipType == "Upgrade")
            {
                return true;
            }

            return false;
        }
    }
}
