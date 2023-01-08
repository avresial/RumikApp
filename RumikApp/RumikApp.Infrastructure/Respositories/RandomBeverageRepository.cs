using RumikApp.Infrastructure.Dto;
using RumikApp.Infrastructure.Repositories;
using RumikApp.Core.Extensions;
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

            for (int i = 0; i < random.Next(10, 15); i++)
            {
                BeverageDtos.Add(new BeverageDto()
                {
                    Icon = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "IMGs\\Bottles\\UnknownBottle.png"))).ConvertBitMapImageToByteArray(),
                    Name = $"Test Name{ random.Next(0, 100)}",
                    AlcoholPercentage = (float)random.NextDouble(),
                    Price = random.Next(1, 100),
                    Grade = random.Next(1, 100),
                    GradeWithCoke = random.Next(1, 100),
                    Capacity = random.Next(1, 100),

                    Nuts = random.Next(1, 100) % 2 == 0,
                    Vanila = random.Next(1, 100) % 2 == 0,
                    Caramel = random.Next(1, 100) % 2 == 0,
                    Smoke = random.Next(1, 100) % 2 == 0,
                    Cinnamon = random.Next(1, 100) % 2 == 0,
                    Spirit = random.Next(1, 100) % 2 == 0,
                    Fruits = random.Next(1, 100) % 2 == 0,
                    Honey = random.Next(1, 100) % 2 == 0,
                    BeAPirate = random.Next(1, 100) % 2 == 0
                });
            }

            return BeverageDtos.Where(x => selector.Invoke(x));
        }

        public async Task<IEnumerable<BeverageDto>> BrowseAll()
        {
            List<BeverageDto> BeverageDtos = new List<BeverageDto>();

            int elementsCount = random.Next(1, 15);
            elementsCount = 10;
            for (int i = 0; i < elementsCount; i++)
            {
                BeverageDtos.Add(new BeverageDto()
                {
                    Icon = new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory, "IMGs\\Bottles\\UnknownBottle.png"))).ConvertBitMapImageToByteArray(),
                    Name = $"Test Name{ random.Next(0, 100)}",
                    AlcoholPercentage = (float)random.NextDouble(),
                    Price = random.Next(1, 100),
                    Grade = random.Next(1, 100),
                    GradeWithCoke = random.Next(1, 100),
                    Capacity = random.Next(1, 100),

                    Nuts = random.Next(1, 100) % 2 == 0,
                    Vanila = random.Next(1, 100) % 2 == 0,
                    Caramel = random.Next(1, 100) % 2 == 0,
                    Smoke = random.Next(1, 100) % 2 == 0,
                    Cinnamon = random.Next(1, 100) % 2 == 0,
                    Spirit = random.Next(1, 100) % 2 == 0,
                    Fruits = random.Next(1, 100) % 2 == 0,
                    Honey = random.Next(1, 100) % 2 == 0,
                    BeAPirate = random.Next(1, 100) % 2 == 0
                });
            }

            return BeverageDtos;
        }

        public Task<bool> RemoveFromRepository(BeverageDto beverageDto)
        {
            throw new NotImplementedException();
        }

        public Task SaveToRepository(BeverageDto beverageDto)
        {
            throw new NotImplementedException();
        }
    }
}
