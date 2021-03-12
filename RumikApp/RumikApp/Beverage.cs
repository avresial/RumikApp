using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp
{
    public class Beverage : ViewModelBase
    {
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;

                _Name = value;
                RaisePropertyChanged("Name");
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

                _Capacity = value;
                RaisePropertyChanged("Capacity");
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
                RaisePropertyChanged("AlcoholPercentage");
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
                RaisePropertyChanged("Grade");
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
                RaisePropertyChanged("GradeWithCoke");
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
                RaisePropertyChanged("Color");
            }
        }

        private bool _Vanila;
        public bool Vanila
        {
            get { return _Vanila; }
            set
            {
                if (_Vanila == value)
                    return;

                _Vanila = value;
                RaisePropertyChanged("Vanila");
            }
        }

        private bool _Nuts;
        public bool Nuts
        {
            get { return _Nuts; }
            set
            {
                if (_Nuts == value)
                    return;

                _Nuts = value;
                RaisePropertyChanged("Nuts");
            }
        }

        private bool _Caramel;
        public bool Caramel
        {
            get { return _Caramel; }
            set
            {
                if (_Caramel == value)
                    return;

                _Caramel = value;
                RaisePropertyChanged("Caramel");
            }
        }

        private bool _Smoke;
        public bool Smoke
        {
            get { return _Smoke; }
            set
            {
                if (_Smoke == value)
                    return;

                _Smoke = value;
                RaisePropertyChanged("Smoke");
            }
        }

        private bool _Cinnamon;
        public bool Cinnamon
        {
            get { return _Cinnamon; }
            set
            {
                if (_Cinnamon == value)
                    return;

                _Cinnamon = value;
                RaisePropertyChanged("Cinnamon");
            }
        }

        private bool _Nutmeg;
        public bool Nutmeg
        {
            get { return _Nutmeg; }
            set
            {
                if (_Nutmeg == value)
                    return;

                _Nutmeg = value;
                RaisePropertyChanged("Nutmeg");
            }
        }

        private bool _Fruits;
        public bool Fruits
        {
            get { return _Fruits; }
            set
            {
                if (_Fruits == value)
                    return;

                _Fruits = value;
                RaisePropertyChanged("Fruits");
            }
        }

        private bool _Honey;
        public bool Honey
        {
            get { return _Honey; }
            set
            {
                if (_Honey == value)
                    return;

                _Honey = value;
                RaisePropertyChanged("Honey");
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

                _Price = value;
                RaisePropertyChanged("Price");
            }
        }

    }
}
