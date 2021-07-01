namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class DefaultBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => string.Empty;

        public (bool isSatisfied, string message) Process(Order order)
        {
            return (true, $"{NameRequirement}: No action");
        }
    }
}
