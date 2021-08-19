using RumikApp.Core.Domain;
using RumikApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Respositories
{
    public class MySQLBeverageRepository : IBeverageRepository
    {
        public Task<IEnumerable<Beverage>> Browse(Func<Beverage, bool> selector)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Beverage>> BrowseAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Beverage>> BrowseByAlcoholPercentageRange(int from, int to)
        {
            throw new NotImplementedException();
        }
    }
}
