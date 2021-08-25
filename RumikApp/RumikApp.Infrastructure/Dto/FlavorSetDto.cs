using RumikApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Dto
{
   public class FlavorSetDto
    {
        public Flavour Vanila { get; set; }
        public Flavour Nuts { get; set; }
        public Flavour Caramel { get; set; }
        public Flavour Smoke { get; set; }
        public Flavour Cinnamon { get; set; }
        public Flavour Spirit { get; set; }
        public Flavour Fruits { get; set; }
        public Flavour Honey { get; set; }
        public Flavour BeAPirate { get; set; }

    }
}
