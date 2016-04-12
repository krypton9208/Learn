using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity;
using NHibernate.AspNet.Identity.Helpers;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Learn.Models.NHibernate
{
    public class NHibernateSession 
    {
        public static ISession OpenSession()
        {
            var myEntities = new[] {
                typeof(LearnUser) };

            var configuration = new Configuration().Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NHibernate.cfg.xml"));
            //configuration.AddDeserializedMapping(MappingHelper.GetIdentityMappings(myEntities), null);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            var factory = configuration.BuildSessionFactory();
            var session = factory.OpenSession();

            var userManager = new UserManager<LearnUser>(
                new UserStore<LearnUser>(session));
            return sessionFactory.OpenSession();
        }
    }
}