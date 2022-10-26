using RumikApp.Infrastructure.Dto;
using System.Diagnostics;

namespace RumikApp.Infrastructure.Extensions
{
    public static class BeverageExtension
    {
        public static Beverage BeverageDtoToBeverage(this BeverageDto beverageDto)
        {
            Beverage beverage = new Beverage();

            beverage.AlcoholPercentage = beverageDto.AlcoholPercentage;
            beverage.Grade = beverageDto.Grade;
            beverage.Name = beverageDto.Name;
            beverage.GradeWithCoke = beverageDto.GradeWithCoke;
            beverage.Price = beverageDto.Price;
            beverage.PricePer100ml = beverageDto.PricePer100ml;
            beverage.Capacity = beverageDto.Capacity;
            beverage.TestIcon = beverageDto.TestIcon;

            beverage.Nuts.IsSet = beverageDto.Nuts;
            beverage.Vanila.IsSet = beverageDto.Vanila;

            beverage.Caramel.IsSet = beverageDto.Caramel;
            beverage.Smoke.IsSet = beverageDto.Smoke;
            beverage.Cinnamon.IsSet = beverageDto.Cinnamon;
            beverage.Spirit.IsSet = beverageDto.Spirit;
            beverage.Fruits.IsSet = beverageDto.Fruits;
            beverage.Honey.IsSet = beverageDto.Honey;
            beverage.BeAPirate.IsSet = beverageDto.BeAPirate;

            Debug.WriteLine("WARNING - BeverageExtension is not finished");

            return beverage;
        }
    }
}
