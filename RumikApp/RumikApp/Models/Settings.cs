using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Models
{
    /// <summary>
    /// Class containing all app settings.
    /// User ID is meant to be a unique PC because there is no loging system yet.
    /// </summary>
    public class Settings : ViewModelBase
    {
        private Guid _UserID;
        public Guid UserID
        {
            get { return _UserID; }
            set
            {
                if (_UserID == value)
                    return;

                _UserID = value;
                RaisePropertyChanged(nameof(UserID));
            }
        }

        private bool _OfflineMode;
        public bool OfflineMode
        {
            get { return _OfflineMode; }
            set
            {
                if (_OfflineMode == value)
                    return;

                _OfflineMode = value;
                RaisePropertyChanged(nameof(OfflineMode));
            }
        }

        private bool _IsUserAbove18;
        public bool IsUserAbove18
        {
            get { return _IsUserAbove18; }
            set
            {
                if (_IsUserAbove18 == value)
                    return;

                _IsUserAbove18 = value;
                RaisePropertyChanged(nameof(IsUserAbove18));
            }
        }


        public Settings()
        {
            _UserID = Guid.NewGuid();
            _OfflineMode = true;
            _IsUserAbove18 = false;
        }
    }
}
