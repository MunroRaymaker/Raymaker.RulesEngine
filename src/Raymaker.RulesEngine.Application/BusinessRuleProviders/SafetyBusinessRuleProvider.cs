namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class SafetyBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "Safety";

        // If the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip).
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(Video) &&
                order.Product?.Name == "Learning to Ski")
            {
                order.PackingSlip += "First Aid";
                return true;
            }
            return false;
        }
    }
}
