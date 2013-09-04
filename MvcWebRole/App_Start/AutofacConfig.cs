using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DataAccess;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MvcWebRole.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            RegisterDal(builder);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterDal(ContainerBuilder builder)
        {
            builder.RegisterType<AzureMailingListRepository>()
                .WithParameter(new PositionalParameter(0, RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString")))
                .As<IMailingListRepository>();
        }
    }
}