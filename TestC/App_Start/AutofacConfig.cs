using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using TestC.Entity;

namespace TestC.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new BLLModule());
            builder.RegisterModule(new RepositoryModule());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private class EFModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {

                builder.RegisterType(typeof(TEST_C_DBEntities)).As(typeof(DbContext)).InstancePerLifetimeScope();
               // builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            }
        }

        private class BLLModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(Assembly.Load("TestC"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces();
                //.InstancePerLifetimeScope();
            }
        }

        private class RepositoryModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(Assembly.Load("TestC"))
                       .Where(t => t.Name.EndsWith("Repository"))
                       .AsImplementedInterfaces();
                //.InstancePerLifetimeScope();
            }
        }

    }
}