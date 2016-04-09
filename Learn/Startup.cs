using Autofac;
using Autofac.Integration.Mvc;
using Learn.Models;
using Learn.Models.NHibernate;
using Learn.Repos.Abstract;
using Learn.Repos.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Web;
using System.Web.Mvc;

namespace Learn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // REGISTER DEPENDENCIES
            // builder.RegisterType<NHibernateSession>().AsSelf().InstancePerRequest();
            builder.RegisterType<EmployeesRepository>().As<IRepository<Employee>>().InstancePerRequest();
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