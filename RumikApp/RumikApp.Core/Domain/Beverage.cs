using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Core.Domain
{
    public class Beverage
    {
        public Guid Id { get; }
        public string Name { get; }

        public Beverage(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
