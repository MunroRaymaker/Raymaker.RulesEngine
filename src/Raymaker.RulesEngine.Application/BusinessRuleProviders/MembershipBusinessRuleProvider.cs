namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class MembershipBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "Membership";

        // If the payment is for a membership, activate that membership.
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(MembershipProduct))
            {
                (order.Product as MembershipProduct).IsActive = true;
                return true;
            }

            return false;
        }
    }
}
