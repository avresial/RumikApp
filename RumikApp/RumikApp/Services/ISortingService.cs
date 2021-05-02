using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public interface ISortingService
    {
        IList<Beverage> SortedBeverages { get; set; }
        IList<Beverage> OriginalBeverages { set; }
        void SortByName();


    }
}
