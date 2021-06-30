namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class DefaultBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => string.Empty;

        public bool Process(ref Order order)
        {
            return true;
        }
    }
}
