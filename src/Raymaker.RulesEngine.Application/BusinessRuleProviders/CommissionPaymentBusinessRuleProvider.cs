using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class CommissionPaymentBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public CommissionPaymentBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement => "Commission";

        public (bool isSatisfied, string message) Process(Order order)
        {
            if (order.Product.GetType() == typeof(Book) ||
                order.Product.GetType() == typeof(Video))
            {
                var agent = new Agent{ Name = order.SoldBy, Commission = 10 };
                var result = this.userService.UpdateAgent(agent);

                return (result, $"{NameRequirement}: Added commission payment");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
