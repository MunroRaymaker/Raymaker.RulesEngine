using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using System.Collections.Generic;

namespace Raymaker.RulesEngine.Application
{
    public class OrderProcessingService
    {
        private readonly IEnumerable<IBusinessRuleProvider> rules;
        private readonly BusinessRuleProviderFactory businessRuleProviderFactory;

        public OrderProcessingService(IUserService userService)
        {
            this.businessRuleProviderFactory = new BusinessRuleProviderFactory(userService);
            this.rules = businessRuleProviderFactory.GetProviders();
        }

        /// <summary>
        /// Assumes a payment has taken place so we process the order.
        /// </summary>
        public void Process(Order order)
        {
            foreach (IBusinessRuleProvider rule in rules)
            {
                var (isSatisfied, message) = rule.Process(order);
                System.Console.WriteLine(message + ". Result:" + isSatisfied);
            }
        }
    }
}
