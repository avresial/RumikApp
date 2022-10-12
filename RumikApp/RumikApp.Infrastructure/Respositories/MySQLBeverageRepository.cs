using RumikApp.Infrastructure.Dto;
using RumikApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Respositories
{
    public class MySQLBeverageRepository : IBeverageRepository
    {
        public Task<IEnumerable<BeverageDto>> Browse(Func<BeverageDto, bool> selector)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BeverageDto>> BrowseAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BeverageDto>> BrowseByAlcoholPercentageRange(int from, int to)
        {
            throw new NotImplementedException();
        }
    }
}
