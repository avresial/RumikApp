using System.Windows.Media.Imaging;

namespace RumikApp.Infrastructure.Dto
{
    public class BeverageDto : FlavorSetDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public float AlcoholPercentage { get; set; }
        public float Price { get; set; }
        public float PricePer100ml { get; set; }
        public int Grade { get; set; }
        public int GradeWithCoke { get; set; }
        public string Color { get; set; }
        public BitmapImage TestIcon { get; set; }
    }
}
