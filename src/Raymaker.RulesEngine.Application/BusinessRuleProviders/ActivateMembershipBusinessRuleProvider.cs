using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class ActivateMembershipBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public ActivateMembershipBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement => "Membership";

        // If the payment is for a membership, activate that membership.
        public (bool isSatisfied, string message) Process(Order order)
        {
            if (order.Product.GetType() == typeof(Membership))
            {
                var product = order.Product as Membership;
                product.IsActive = true;    

                var user = userService.GetUser((order.Product as Membership).MemberName);
                user.IsActive = true;
                var result = this.userService.UpdateUser(user);

                return (result, $"{NameRequirement}: Membership activated");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
