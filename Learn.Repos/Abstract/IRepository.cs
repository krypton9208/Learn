using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll { get;}
        int Add(T t);
        bool Update(T t);
        bool Delete(T t);
        T GetById( int t);
    }
}
