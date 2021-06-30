namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class BookProductBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement => "BookProduct";

        // If the payment is for a book, create a duplicate packing slip for the royalty department.
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(Book))
            {
                if (!string.IsNullOrEmpty(order.PackingSlip) && 
                    order.PackingSlip.Contains("royalty"))
                {
                    return true;
                }
                order.PackingSlip += "royalty";
                return true;
            }

            return false;
        }
    }
}
