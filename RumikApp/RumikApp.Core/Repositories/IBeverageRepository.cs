using RumikApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Core.Repositories
{
    public interface IBeverageRepository
    {
        Task<IEnumerable<Beverage>> BrowseAll(  );
        Task<IEnumerable<Beverage>> BrowseByAlcoholPercentageRange(int from, int to);
        Task<IEnumerable<Beverage>> Browse(Func<Beverage, bool> selector);

    }
}
