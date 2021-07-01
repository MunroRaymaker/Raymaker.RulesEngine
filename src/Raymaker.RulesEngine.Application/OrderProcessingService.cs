using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using System.Collections.Generic;

namespace Raymaker.RulesEngine.Application
{
    public class OrderProcessingService
    {
        private readonly IEnumerable<IBusinessRuleProvider> rules;

        public OrderProcessingService(IEnumerable<IBusinessRuleProvider> rules)
        {
            this.rules = rules;
        }

        /// <summary>
        /// Assumes a payment has taken place so we process the order.
        /// </summary>
        public void Process(Order order)
        {
            foreach (IBusinessRuleProvider rule in rules)
            {
                var (isSatisfied, message) = rule.Process(order);
                System.Console.WriteLine(message);
            }
        }
    }
}
