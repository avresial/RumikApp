using RumikApp.Infrastructure.Dto;
using System.Diagnostics;

namespace RumikApp.Infrastructure.Extensions
{
    public static class BeverageExtension
    {
        public static Beverage BeverageDtoToBeverage(this BeverageDto beverageDto)
        {
            Beverage beverage = new Beverage();

            beverage.ID = beverageDto.ID;
            beverage.Name = beverageDto.Name;
            beverage.Capacity = beverageDto.Capacity;
            beverage.AlcoholPercentage = beverageDto.AlcoholPercentage;
            beverage.Price = beverageDto.Price;
            beverage.PricePer100ml = beverageDto.PricePer100ml;
            beverage.Grade = beverageDto.Grade;
            beverage.GradeWithCoke = beverageDto.GradeWithCoke;
            beverage.Color = beverageDto.Color;
            beverage.TestIcon = beverageDto.TestIcon;

            beverage.Vanila.IsSet = beverageDto.Vanila;
            beverage.Nuts.IsSet = beverageDto.Nuts;
            beverage.Caramel.IsSet = beverageDto.Caramel;
            beverage.Smoke.IsSet = beverageDto.Smoke;
            beverage.Cinnamon.IsSet = beverageDto.Cinnamon;
            beverage.Spirit.IsSet = beverageDto.Spirit;
            beverage.Fruits.IsSet = beverageDto.Fruits;
            beverage.Honey.IsSet = beverageDto.Honey;
            beverage.BeAPirate.IsSet = beverageDto.BeAPirate;

            return beverage;
        }
    }
}
