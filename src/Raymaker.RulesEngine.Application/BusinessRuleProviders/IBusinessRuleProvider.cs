namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public interface IBusinessRuleProvider
    {
        public bool Process(ref Order order);
        public string NameRequirement { get; }
    }
}
