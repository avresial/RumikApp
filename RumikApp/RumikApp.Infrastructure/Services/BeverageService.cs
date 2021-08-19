using RumikApp.Core.Repositories;
using RumikApp.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace RumikApp.Infrastructure.Services
{
    public class BeverageService : IBeverageService
    {
        private readonly IBeverageRepository beverageRepository;

        public BeverageService(IBeverageRepository beverageRepository)
        {
            this.beverageRepository = beverageRepository;
        }

        public async Task<IEnumerable<BeverageDto>> BrowseAll()
        {
            var beverages = await this.beverageRepository.BrowseAll();

            return beverages.Select(b =>
                new BeverageDto
                {
                    Id = b.Id,
                    Name = b.Name
                }
            );
        }

        public async Task<BeverageDto> PickRandomBeverage()
        {
            var beverages = await this.beverageRepository.BrowseAll();

            // Logika losowania rumu
            var item = beverages.First();

            return new BeverageDto
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
