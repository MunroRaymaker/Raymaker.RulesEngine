using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raymaker.RulesEngine.Application
{
    public class OrderProcessingService
    {
        private readonly IEnumerable<IBusinessRuleProvider> rules;

        public OrderProcessingService(IEnumerable<IBusinessRuleProvider> rules)
        {
            this.rules = rules;
        }

        public void Process(Order order)
        {
            foreach (IBusinessRuleProvider rule in rules)
            {
                rule.Process(order);
            }
        }
    }
}
