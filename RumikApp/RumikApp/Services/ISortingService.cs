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
        IList<Beverage> OriginalBeverages { get; set; }
        void ShowDefault();
        void SortByNameAscending(); 
        void SortByNameDescending();
        void SortByPriceAscending();
        void SortByPriceDescending();
        void SortByGradeAscending(); 
        void SortByGradeDescending();
        void SortByGradeWithCokeAscending();
        void SortByGradeWithCokeDescending();
    }
}
