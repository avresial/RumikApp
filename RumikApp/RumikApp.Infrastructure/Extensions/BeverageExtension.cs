using RumikApp.Core.Extensions;
using RumikApp.Infrastructure.Dto;

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
            beverage.Icon = beverageDto.Icon.ConvertToBitMapImage();

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

        public static BeverageDto BeverageoToBeverageDto(this Beverage beverage)
        {
            BeverageDto BeverageDto = new BeverageDto();

            BeverageDto.ID = beverage.ID;
            BeverageDto.Name = beverage.Name;
            BeverageDto.Capacity = beverage.Capacity;
            BeverageDto.AlcoholPercentage = beverage.AlcoholPercentage;
            BeverageDto.Price = beverage.Price;
            BeverageDto.PricePer100ml = beverage.PricePer100ml;
            BeverageDto.Grade = beverage.Grade;
            BeverageDto.GradeWithCoke = beverage.GradeWithCoke;
            BeverageDto.Color = beverage.Color;
            BeverageDto.Icon = beverage.Icon.ConvertBitMapImageToByteArray();
            
            BeverageDto.Vanila = beverage.Vanila.IsSet;
            BeverageDto.Nuts = beverage.Nuts.IsSet;
            BeverageDto.Caramel = beverage.Caramel.IsSet;
            BeverageDto.Smoke = beverage.Smoke.IsSet;
            BeverageDto.Cinnamon = beverage.Cinnamon.IsSet;
            BeverageDto.Spirit = beverage.Spirit.IsSet;
            BeverageDto.Fruits = beverage.Fruits.IsSet;
            BeverageDto.Honey = beverage.Honey.IsSet;
            BeverageDto.BeAPirate = beverage.BeAPirate.IsSet;

            return BeverageDto;
        }
    }
}
