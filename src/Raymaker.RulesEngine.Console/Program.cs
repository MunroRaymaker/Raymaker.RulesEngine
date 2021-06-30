using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.BusinessRuleProviders;
using System;

namespace Raymaker.RulesEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            // Build an order
            var order = new Order
            {
                PackingSlip = "",
                Payment = new Payment(),
                Product = new MembershipProduct { IsActive = true, MemberEmail = "", MenbershipType = "Basic", Name = "BasicMembership", UnitPrice = 100 }
            };

            // Get business rules
            var rules = new BusinessRuleProviderFactory().GetProviders();

            // Execute rules

        }
    }
}
