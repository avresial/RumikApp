using RumikApp.Core.Domain;
using RumikApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Respositories
{
    public class InMemoryBeverageRepository : IBeverageRepository
    {
        private List<Beverage> beverages;

        public InMemoryBeverageRepository()
        {
            this.beverages = new List<Beverage>()
            {
               new Beverage(Guid.NewGuid(), "Rum"),
               new Beverage(Guid.NewGuid(), "Rum"),
               new Beverage(Guid.NewGuid(), "Rum"),
               new Beverage(Guid.NewGuid(), "Rum"),
               new Beverage(Guid.NewGuid(), "Rum")
            };
        }

        public Task<IEnumerable<Beverage>> Browse(Func<Beverage, bool> selector)
            => Task.FromResult(beverages.Where(selector));

        public Task<IEnumerable<Beverage>> BrowseAll()
            => Task.FromResult(beverages.AsEnumerable<Beverage>());

        public Task<IEnumerable<Beverage>> BrowseByAlcoholPercentageRange(int from, int to)
        {
            throw new NotImplementedException();
        }
    }
}
