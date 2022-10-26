using RumikApp.Infrastructure.Dto;
using RumikApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RumikApp.Infrastructure.Respositories
{
    public class RandomBeverageRepository : IBeverageRepository
    {
        Random random = new Random();
        public async Task<IEnumerable<BeverageDto>> Browse(Func<BeverageDto, bool> selector)
        {
            List<BeverageDto> BeverageDtos = new List<BeverageDto>();

            for (int i = 0; i < random.Next(1, 15); i++)
            {
                BeverageDtos.Add(new BeverageDto()
                {
                    TestIcon = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "IMGs\\Bottles\\UnknownBottle.png"))),
                    Name = $"Test Name{ random.Next(0, 100)}",
                    AlcoholPercentage = (float)random.NextDouble(),
                    Price = random.Next(1, 100),
                    Grade = random.Next(1, 100),
                    GradeWithCoke = random.Next(1, 100),
                    Capacity = random.Next(1, 100)
                });
            }

            return BeverageDtos.Where(x => selector.Invoke(x));
        }

        public async Task<IEnumerable<BeverageDto>> BrowseAll()
        {
            List<BeverageDto> BeverageDtos = new List<BeverageDto>();
            for (int i = 0; i < random.Next(1, 15); i++)
            {
                BeverageDtos.Add(new BeverageDto()
                {
                    TestIcon = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "IMGs\\Bottles\\UnknownBottle.png"))),
                    Name = $"Test Name{ random.Next(0, 100)}",
                    AlcoholPercentage = (float)random.NextDouble(),
                    Price = random.Next(1, 100),
                    Grade = random.Next(1, 100),
                    GradeWithCoke = random.Next(1, 100),
                    Capacity = random.Next(1, 100)
                });
            }

            return BeverageDtos;
        }
    }
}
