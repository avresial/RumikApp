﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ApprovalToolForRumikApp
{
    public class Beverage : ViewModelBase
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

                if (/*value != null &&*/ value != 0)
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

               // if (value != null)
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

        private String _Color;
        public String Color
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

        private Flavour _Vanila = new Flavour("/IMGs/PollIMG/Vanila.png", "Vanila");
        public Flavour Vanila
        {
            get { return _Vanila; }
            set
            {
                if (_Vanila == value)
                    return;

                _Vanila = value;
                RaisePropertyChanged(nameof(Vanila));
            }
        }

        private Flavour _Nuts = new Flavour("/IMGs/PollIMG/Nuts.png", "Nuts");
        public Flavour Nuts
        {
            get { return _Nuts; }
            set
            {
                if (_Nuts == value)
                    return;

                _Nuts = value;
                RaisePropertyChanged(nameof(Nuts));
            }
        }

        private Flavour _Caramel = new Flavour("/IMGs/PollIMG/Carmel.png", "Caramel");
        public Flavour Caramel
        {
            get { return _Caramel; }
            set
            {
                if (_Caramel == value)
                    return;

                _Caramel = value;
                RaisePropertyChanged(nameof(Caramel));
            }
        }

        private Flavour _Smoke = new Flavour("/IMGs/PollIMG/Smoked.png", "Smoke");
        public Flavour Smoke
        {
            get { return _Smoke; }
            set
            {
                if (_Smoke == value)
                    return;

                _Smoke = value;
                RaisePropertyChanged(nameof(Smoke));
            }
        }

        private Flavour _Cinnamon = new Flavour("/IMGs/PollIMG/Cinamon.png", "Cinnamon");
        public Flavour Cinnamon
        {
            get { return _Cinnamon; }
            set
            {
                if (_Cinnamon == value)
                    return;

                _Cinnamon = value;
                RaisePropertyChanged(nameof(Cinnamon));
            }
        }

        private Flavour _Spirit = new Flavour("/IMGs/PollIMG/Spirit.png", "Spirit");
        public Flavour Spirit
        {
            get { return _Spirit; }
            set
            {
                if (_Spirit == value)
                    return;

                _Spirit = value;
                RaisePropertyChanged(nameof(Spirit));
            }
        }

        private Flavour _Fruits = new Flavour("/IMGs/PollIMG/Fruits.png", "Fruits");
        public Flavour Fruits
        {
            get { return _Fruits; }
            set
            {
                if (_Fruits == value)
                    return;

                _Fruits = value;
                RaisePropertyChanged(nameof(Fruits));
            }
        }

        private Flavour _Honey = new Flavour("/IMGs/PollIMG/Honey.png", "Honey");
        public Flavour Honey
        {
            get { return _Honey; }
            set
            {
                if (_Honey == value)
                    return;

                _Honey = value;

                RaisePropertyChanged(nameof(Honey));
            }
        }

        private Flavour _BeAPirate = new Flavour("/IMGs/PollIMG/BeAPirate.png", "Honey");
        public Flavour BeAPirate
        {
            get { return _BeAPirate; }
            set
            {
                if (_BeAPirate == value)
                    return;

                _BeAPirate = value;

                RaisePropertyChanged(nameof(BeAPirate));
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
                _Vanila.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _Nuts.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _Caramel.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _Smoke.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _Cinnamon.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _Spirit.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _Fruits.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                _BeAPirate.IsSet = true;
            if (((int)rand.Next(0, 20)) % 2 == 0)
                Honey.IsSet = true;


            return this;
        }
    }
}
