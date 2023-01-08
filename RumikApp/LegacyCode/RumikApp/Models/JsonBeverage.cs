using GalaSoft.MvvmLight;
using RumikApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RumikApp
{
    public class JsonBeverage : FlavoursSet
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID == value)
                    return;

                _ID = value;
                RaisePropertyChanged(nameof(ID));
            }
        }

        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;

                _Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private int _Capacity;
        public int Capacity
        {
            get { return _Capacity; }
            set
            {
                if (_Capacity == value)
                    return;

                if (/*value != null && */value != 0)
                    PricePer100ml = (Price / value) * 100;

                _Capacity = value;
                RaisePropertyChanged(nameof(Capacity));
            }
        }

        private float _AlcoholPercentage;
        public float AlcoholPercentage
        {
            get { return _AlcoholPercentage; }
            set
            {
                if (_AlcoholPercentage == value)
                    return;

                _AlcoholPercentage = value;
                RaisePropertyChanged(nameof(AlcoholPercentage));
            }
        }

        private float _Price;
        public float Price
        {
            get { return _Price; }
            set
            {
                if (_Price == value)
                    return;

                //if (value != null)
                    PricePer100ml = (value / Capacity) * 100;

                _Price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        private float _PricePer100ml;
        public float PricePer100ml
        {
            get { return _PricePer100ml; }
            set
            {
                if (_PricePer100ml == value)
                    return;

                _PricePer100ml = value;
                RaisePropertyChanged(nameof(PricePer100ml));
            }
        }

        private int _Grade;
        public int Grade
        {
            get { return _Grade; }
            set
            {
                if (_Grade == value)
                    return;

                _Grade = value;
                RaisePropertyChanged(nameof(Grade));
            }
        }

        private int _GradeWithCoke;
        public int GradeWithCoke
        {
            get { return _GradeWithCoke; }
            set
            {
                if (_GradeWithCoke == value)
                    return;

                _GradeWithCoke = value;
                RaisePropertyChanged(nameof(GradeWithCoke));
            }
        }

        private string _Color;
        public string Color
        {
            get { return _Color; }
            set
            {
                if (_Color == value)
                    return;

                _Color = value;
                RaisePropertyChanged(nameof(Color));
            }
        }
          
        private byte[] _TestIcon;
        public byte[] TestIcon
        {
            get { return _TestIcon; }
            set
            {
                if (_TestIcon == value)
                    return;

                _TestIcon = value;
                RaisePropertyChanged(nameof(TestIcon));
            }
        }

        /// <summary>
        /// Creates instance of bevereage element with random values
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public JsonBeverage GetRandomBevrage(Random rand)
        {
            _Name = "XD" + rand.Next().ToString();
            _Capacity = rand.Next();
            _AlcoholPercentage = rand.Next();
            _Price = rand.Next(0, 200);
            _Grade = rand.Next(0, 10);
            _GradeWithCoke = rand.Next(0, 10);
            _Color = rand.Next().ToString();

            if (((int)rand.Next(0, 20)) % 2 == 0)
                Vanila.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Nuts.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Caramel.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Smoke.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Cinnamon.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Spirit.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Fruits.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                BeAPirate.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Honey.IsSet = true;


            return this;
        }

        static public Beverage TransFromJsonBeverageToBeverage(JsonBeverage jsonBeverage)
        {
            Beverage newBeverage = new Beverage();

            newBeverage.ID = jsonBeverage.ID;
            newBeverage.Name = jsonBeverage.Name;
            newBeverage.Capacity = jsonBeverage.Capacity;
            newBeverage.AlcoholPercentage = jsonBeverage.AlcoholPercentage;
            newBeverage.Price = jsonBeverage.Price;
            newBeverage.PricePer100ml = jsonBeverage.PricePer100ml;
            newBeverage.Grade = jsonBeverage.Grade;
            newBeverage.GradeWithCoke = jsonBeverage.GradeWithCoke;
            newBeverage.Color = jsonBeverage.Color;

            newBeverage.Vanila = jsonBeverage.Vanila;
            newBeverage.Nuts = jsonBeverage.Nuts;
            newBeverage.Caramel = jsonBeverage.Caramel;
            newBeverage.Smoke = jsonBeverage.Smoke;
            newBeverage.Cinnamon = jsonBeverage.Cinnamon;
            newBeverage.Spirit = jsonBeverage.Spirit;
            newBeverage.Fruits = jsonBeverage.Fruits;
            newBeverage.Honey = jsonBeverage.Honey;
            newBeverage.BeAPirate = jsonBeverage.BeAPirate;
                      

            newBeverage.TestIcon = ImageProcessingService.ConvertToBitMapImage(jsonBeverage.TestIcon);


            return newBeverage;
        }

        static public JsonBeverage TransFromBeverageToJsonBeverage(Beverage Beverage, byte[] img)
        {
            JsonBeverage newJsonBeverage = new JsonBeverage();

            newJsonBeverage.ID  = Beverage.ID;
            newJsonBeverage.Name = Beverage.Name;
            newJsonBeverage.Capacity = Beverage.Capacity;
            newJsonBeverage.AlcoholPercentage = Beverage.AlcoholPercentage;
            newJsonBeverage.Price = Beverage.Price;
            newJsonBeverage.PricePer100ml = Beverage.PricePer100ml;
            newJsonBeverage.Grade = Beverage.Grade;
            newJsonBeverage.GradeWithCoke = Beverage.GradeWithCoke;
            newJsonBeverage.Color = Beverage.Color;
         
            newJsonBeverage.Vanila = Beverage.Vanila;
            newJsonBeverage.Nuts = Beverage.Nuts;
            newJsonBeverage.Caramel = Beverage.Caramel;
            newJsonBeverage.Smoke = Beverage.Smoke;
            newJsonBeverage.Cinnamon = Beverage.Cinnamon;
            newJsonBeverage.Spirit = Beverage.Spirit;
            newJsonBeverage.Fruits = Beverage.Fruits;
            newJsonBeverage.Honey = Beverage.Honey;
            newJsonBeverage.BeAPirate = Beverage.BeAPirate;

            newJsonBeverage.TestIcon = img;

            return newJsonBeverage;
        }

        static public JsonBeverage TransFromBeverageToJsonBeverage(Beverage Beverage)
        {
            JsonBeverage newJsonBeverage = new JsonBeverage();

            newJsonBeverage.ID = Beverage.ID;
            newJsonBeverage.Name = Beverage.Name;
            newJsonBeverage.Capacity = Beverage.Capacity;
            newJsonBeverage.AlcoholPercentage = Beverage.AlcoholPercentage;
            newJsonBeverage.Price = Beverage.Price;
            newJsonBeverage.PricePer100ml = Beverage.PricePer100ml;
            newJsonBeverage.Grade = Beverage.Grade;
            newJsonBeverage.GradeWithCoke = Beverage.GradeWithCoke;
            newJsonBeverage.Color = Beverage.Color;

            newJsonBeverage.Vanila = Beverage.Vanila;
            newJsonBeverage.Nuts = Beverage.Nuts;
            newJsonBeverage.Caramel = Beverage.Caramel;
            newJsonBeverage.Smoke = Beverage.Smoke;
            newJsonBeverage.Cinnamon = Beverage.Cinnamon;
            newJsonBeverage.Spirit = Beverage.Spirit;
            newJsonBeverage.Fruits = Beverage.Fruits;
            newJsonBeverage.Honey = Beverage.Honey;
            newJsonBeverage.BeAPirate = Beverage.BeAPirate;

            newJsonBeverage.TestIcon = ImageProcessingService.ConvertBitMapImageToByteArray(Beverage.TestIcon);

            return newJsonBeverage;
        }
    }
}
