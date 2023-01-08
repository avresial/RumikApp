using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalToolForRumikApp
{
    public class Flavour : ViewModelBase
    {

        private string _IMGSrc;
        public string IMGSrc
        {
            get { return _IMGSrc; }
            private set
            {
                if (_IMGSrc == value)
                    return;

                _IMGSrc = value;
                RaisePropertyChanged(nameof(IMGSrc));
            }
        }

        private bool _IsSet;
        public bool IsSet
        {
            get { return _IsSet; }
            set
            {
                if (_IsSet == value)
                    return;

                _IsSet = value;
                RaisePropertyChanged(nameof(IsSet));
            }
        }

        private string _Name;
        public string Name
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


        public Flavour(string imgSrc, string name)
        {
            _IMGSrc = imgSrc;
            _Name = name;
        }
    }
}
