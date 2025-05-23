using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy
{
    public class User
    {
        public string UserName { get; set; }
        public string Role { get; set; }

        public User(string username, string role)
        {
            UserName = username;
            Role = role;
        }

        public override string ToString()
        {
            return ($"Username:{UserName} | Role: {Role}");
        }
    }
}
