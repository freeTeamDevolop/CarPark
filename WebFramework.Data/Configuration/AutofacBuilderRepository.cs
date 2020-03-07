using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Data.Infrastructure;

namespace WebFramework.Data.Configuration
{
    public class AutofacBuilderRepository
    {
        public static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterType<FrameworkContextLocator>().Keyed<ITransactionManager>("FrameworkContextLocator")
                .As<ITransactionManager>().InstancePerLifetimeScope();
        }
    }
}
