using Autofac;
using Autofac.Integration.Mvc;
using Learn.Models;
using Learn.Repos.Abstract;
using Learn.Repos.Concrete;
using Owin;
using System.Web.Mvc;
using Learn.Logger;
using log4net;
using System;
using log4net.Config;

namespace Learn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // REGISTER DEPENDENCIES
            builder.RegisterType<EmployeesRepository>().As<IEmployeesRepository<Employee>>().InstancePerRequest();
            builder.RegisterGeneric(typeof(LoggerService<>)).As(typeof(ILoggerService<>));
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // BUILD CONTAINER
            var container = builder.Build();

            // SET AUTOFAC DEPENDENCY RESOLVER
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // REGISTER AUTOFAC WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);


        }
    }
}