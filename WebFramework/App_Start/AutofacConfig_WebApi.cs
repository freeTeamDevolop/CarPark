using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace WebFramework.App_Start
{
    public class AutofacConfig_WebApi
    {
        public static void Regist()
        {
            // 实例化一个autofac的创建容器

            var builder = new ContainerBuilder();

            // 注册api容器需要使用HTTPConfiguration对象

            HttpConfiguration config = GlobalConfiguration.Configuration;

            SetupResolveRules(builder);

            //注册所有的ApiControllers

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            // 创建一个autofac的容器

            var container = builder.Build();

            // api的控制器对象由autofac来创建

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        /// <summary>

        /// 设置配置规则

        /// </summary>

        /// <param name="builder"></param>

        public static void SetupResolveRules(ContainerBuilder builder)

        {

            // 告诉autofac框架，将来要创建的控制器类存放在哪个程序集,(本实例[CommonManagement.Web])

            Assembly controllerAssmbly = Assembly.Load("WebFramework.WebApiControllers");

            builder.RegisterApiControllers(controllerAssmbly);

            //// 如果需要直接调用仓储层

            //// 告诉autofac框架注册数据仓储层所在程序集中的所有类的对象实例

            //Assembly RepositoryAssembly = Assembly.Load("CommonManagement.Repository");

            //// 创建仓储层中的所有类的instance以此类的实现接口存储

            //builder.RegisterTypes(RepositoryAssembly.GetTypes()).Where(a => a.Name.Contains("Repository")).AsImplementedInterfaces();



            // 告诉autofac框架注册数据业务层(应用层)所在程序集中的所有类的对象实例

            Assembly ServiceAssembly = Assembly.Load("WebFramework.WebApiControllers");

            // 创建应用层中的所有类的instance以此类的实现接口存储

            builder.RegisterTypes(ServiceAssembly.GetTypes()).Where(a => a.Name.Contains("Service")).AsImplementedInterfaces();

        }
    }
}