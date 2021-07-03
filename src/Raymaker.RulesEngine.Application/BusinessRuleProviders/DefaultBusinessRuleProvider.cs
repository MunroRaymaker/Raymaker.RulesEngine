using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class DefaultBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public DefaultBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement => string.Empty;

        public (bool isSatisfied, string message) Process(Order order)
        {
            return (true, $"{NameRequirement}: No action");
        }
    }
}
