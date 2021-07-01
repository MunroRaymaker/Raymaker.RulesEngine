﻿namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
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
                (order.Product as Membership).IsActive = true;
                var email = (order.Product as Membership).MemberEmail;
                var userName = (order.Product as Membership).MemberName;

                this.userService.UpdateUser(new User
                {
                    Email = email,
                    IsActive = true,
                    MembershipType = MembershipType.Basic,
                    UserName = userName
                });

                return (true, $"{NameRequirement}: Membership activated");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
