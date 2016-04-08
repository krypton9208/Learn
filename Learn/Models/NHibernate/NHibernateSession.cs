using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Models.NHibernate
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\NHibernate.cfg.xml");
            configuration.Configure(configurationPath);
            //var configurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Mapping\Employee.hbm.xml");
            //configuration.AddFile(configurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}