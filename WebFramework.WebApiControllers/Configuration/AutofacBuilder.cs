using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.WebApiControllers.Services.CarPark;
using WebFramework.WebApiControllers.Services.Users;

namespace WebFramework.WebApiControllers.Configuration
{
    public class AutofacBuilder
    {
        public static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>().InstancePerDependency();
            builder.RegisterType<CarParkService>().As<ICarParkService>().InstancePerDependency();
        }
    }
}
