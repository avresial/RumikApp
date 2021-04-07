using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Classes
{
    public class FlavoursSet : ViewModelBase
    {
        const string imagesLocalization = "/IMGs/PollIMG/";
        const string fileExtension = ".png";

        private Flavour _Vanila = new Flavour(imagesLocalization + nameof(Vanila) + fileExtension, nameof(Vanila));
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

        private Flavour _Nuts = new Flavour(imagesLocalization + nameof(Nuts) + fileExtension, nameof(Nuts));
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

        private Flavour _Caramel = new Flavour(imagesLocalization + nameof(Caramel) + fileExtension, nameof(Caramel));
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

        private Flavour _Smoke = new Flavour(imagesLocalization + nameof(Smoke) + fileExtension, nameof(Smoke));
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

        private Flavour _Cinnamon = new Flavour(imagesLocalization + nameof(Cinnamon) + fileExtension, nameof(Cinnamon));
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

        private Flavour _Spirit = new Flavour(imagesLocalization + nameof(Spirit) + fileExtension, nameof(Spirit));
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

        private Flavour _Fruits = new Flavour(imagesLocalization + nameof(Fruits) + fileExtension, nameof(Fruits));
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

        private Flavour _Honey = new Flavour(imagesLocalization + nameof(Honey) + fileExtension, nameof(Honey));
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

        private Flavour _BeAPirate = new Flavour(imagesLocalization + nameof(BeAPirate) + fileExtension, nameof(BeAPirate));
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

    }
}
