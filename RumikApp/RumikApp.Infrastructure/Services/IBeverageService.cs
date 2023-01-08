using RumikApp.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Infrastructure.Services
{
    public interface IBeverageService
    {
        Task<IEnumerable<BeverageDto>> BrowseAll();
        Task<BeverageDto> PickRandomBeverage();
    }
}
