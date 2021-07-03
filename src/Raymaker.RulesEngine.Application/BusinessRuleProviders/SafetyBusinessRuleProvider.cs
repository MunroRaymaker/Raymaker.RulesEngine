using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class SafetyBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public SafetyBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement => "Safety";

        // If the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip).
        public (bool isSatisfied, string message) Process(Order order)
        {
            if (order.Product.GetType() == typeof(Video) &&
                order.Product?.Name == "Learning to Ski")
            {
                order.PackingSlip += "First Aid";
                return (true, $"{NameRequirement}: Added First Aid video");
            }
            return (false, $"{NameRequirement}: No action");
        }
    }
}
