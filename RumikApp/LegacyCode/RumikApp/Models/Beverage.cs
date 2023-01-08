using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RumikApp
{/// <summary>
///  POCO class
///  in future will implement data annotation
/// </summary>
    public class Beverage : FlavoursSet
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
        [Required(ErrorMessage ="Pole nie może być puste")]
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
        [Required(ErrorMessage ="Pole nie może być puste")]
        [Range(50,10000,ErrorMessage = "Nieprawidłowa pojemonść")]
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

        private BitmapImage _TestIcon;
        public BitmapImage TestIcon
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
        public Beverage GetRandomBevrage(Random rand)
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

        public void Update(Beverage newBeverage) 
        {
            this.Name = newBeverage.Name;
            Capacity = newBeverage.Capacity;
            AlcoholPercentage = newBeverage.AlcoholPercentage;
            Price = newBeverage.Price;
            PricePer100ml = newBeverage.PricePer100ml;
            Grade = newBeverage.Grade;
            Color = newBeverage.Color;
            TestIcon = newBeverage.TestIcon;

            Vanila = newBeverage.Vanila;
            Nuts = newBeverage.Nuts;
            Caramel = newBeverage.Caramel;
            Smoke = newBeverage.Smoke;
            Cinnamon = newBeverage.Cinnamon;
            Spirit = newBeverage.Spirit;
            Fruits = newBeverage.Fruits;
            Honey = newBeverage.Honey;
            BeAPirate = newBeverage.BeAPirate;
        }
    }
}
