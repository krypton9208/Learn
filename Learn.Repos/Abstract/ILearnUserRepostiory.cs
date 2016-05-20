using Learn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Abstract
{
    public interface ILearnUserRepostiory<T> : IRepository<T>
    {
        void AllowToAccess(string login);
    }
}
