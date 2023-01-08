using System.Collections.Generic;

namespace RumikApp.Core.Services
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
