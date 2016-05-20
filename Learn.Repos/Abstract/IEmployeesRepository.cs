﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repos.Abstract
{
    public interface IEmployeesRepository<T> : IRepository<T>
    {
        IEnumerable<T> Search(string search);
    }
}
