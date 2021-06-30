namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public interface IBusinessRuleProvider
    {
        public bool Process(Order order);
        public string NameRequirement { get; }
    }
}
