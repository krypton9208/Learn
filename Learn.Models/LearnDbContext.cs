using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpArch.Domain.DomainModel;

namespace Learn.Models
{
    public class LearnDbContext : Entity
    {
        public LearnUser User { get; set; }
    }
}
