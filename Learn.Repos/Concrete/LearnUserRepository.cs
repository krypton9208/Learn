using Learn.Models;
using Learn.Models.NHibernate;
using Learn.Repos.Abstract;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Concrete
{
    public class LearnUserRepository : Repository<LearnUser>, ILearnUserRepostiory<LearnUser>
    {
        private readonly ISession _session;

        public void AllowToAccess(string login)
        {
            var role =  _session.Get<IdentityRole>("f80a31e7-683e-4090-a04a-f012bd20e0de");
            var user = GetAll.First(x => x.UserName == login);
            if (user != null)
            {
                user.Roles.Add(role);
            }
            Update(user);
        }

        public LearnUserRepository()
        {
            _session = NHibernateSession.OpenSession();
        }
    }
}
