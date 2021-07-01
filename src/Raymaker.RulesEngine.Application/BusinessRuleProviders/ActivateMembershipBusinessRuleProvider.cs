namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class ActivateMembershipBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "Membership";

        // If the payment is for a membership, activate that membership.
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(Membership))
            {
                (order.Product as Membership).IsActive = true;
                return true;
            }

            return false;
        }
    }
}
