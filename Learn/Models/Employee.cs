using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Models
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}