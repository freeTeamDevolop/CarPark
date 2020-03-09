using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.Api.Filters
{
    //表示该方法或控制器只能由特定群组用户访问
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RoleFilter:Attribute
    {
        public List<string> Roles { get; private set; }

        private RoleFilter()
        {
            Roles = new List<string>();
        }

        public RoleFilter(string role)
            : this()
        {
            Roles.Add(role);
        }

        public RoleFilter(string role1, string role2)
            : this(role1)
        {
            Roles.Add(role2);
        }

        public RoleFilter(string role1, string role2, string role3)
            : this(role1, role2)
        {
            Roles.Add(role3);
        }

        public RoleFilter(string role1, string role2, string role3, string role4)
            : this(role1, role2, role3)
        {
            Roles.Add(role4);
        }
    }
}
