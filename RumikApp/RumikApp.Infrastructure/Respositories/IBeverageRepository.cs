using RumikApp.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Repositories
{
    public interface IBeverageRepository
    {
        Task<IEnumerable<BeverageDto>> BrowseAll();
        Task<IEnumerable<BeverageDto>> Browse(Func<BeverageDto, bool> selector);

    }
}
