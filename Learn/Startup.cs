using Autofac;
using Autofac.Integration.Mvc;
using Learn.Models;
using Learn.Repos.Abstract;
using Learn.Repos.Concrete;
using Owin;
using System.Web.Mvc;
using Learn.Logger;
using Microsoft.Owin;
using SharpArch.NHibernate.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(Learn.Startup))]

namespace Learn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            var builder = new ContainerBuilder();
            // REGISTER DEPENDENCIES
            //builder.RegisteType<NHibernateSession>().As<ISession>().InstancePerRequest();
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
            ConfigureData();

        }
        private static void ConfigureData()
        {
            var storage = new WebSessionStorage(System.Web.HttpContext.Current.ApplicationInstance);
            Models.NHibernate.DataConfig.Configure(storage);
        }
    }
}