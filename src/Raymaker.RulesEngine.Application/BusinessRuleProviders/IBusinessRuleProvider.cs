using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public interface IBusinessRuleProvider
    {
        public (bool isSatisfied, string message) Process(Order order);
        public string NameRequirement { get; }
    }
}
