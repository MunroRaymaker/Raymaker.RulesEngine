using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raymaker.RulesEngine.Application
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public MembershipType MembershipType { get; set; }
        public int EmailsSent { get; set; }
    }
}
