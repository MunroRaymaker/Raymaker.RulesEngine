using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using System;

namespace Raymaker.RulesEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Business Rules Engine Sample App");

            // Build an order
            var order = new Order
            {
                PackingSlip = "",
                Payment = new Payment(),
                Product = new MembershipProduct { IsActive = true, MemberEmail = "", MembershipType = "Basic", Name = "BasicMembership", UnitPrice = 100 }
            };

            // Get business rules
            var rules = new BusinessRuleProviderFactory().GetProviders();

            // Execute rules
            var service = new OrderProcessingService(rules);
            service.Process(order);

            System.Console.WriteLine("Rules processed");
            System.Console.WriteLine("Emails sent: " + (order.Product as MembershipProduct).EmailsSent);
        }
    }
}
