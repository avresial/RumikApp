using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalToolForRumikApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private InsertDataToDatabaseFormViewModel _InsertDataToDatabaseFromViewModel;
        public InsertDataToDatabaseFormViewModel InsertDataToDatabaseFromViewModel
        {
            get { return _InsertDataToDatabaseFromViewModel; }
            set
            {
                if (_InsertDataToDatabaseFromViewModel == value)
                    return;

                _InsertDataToDatabaseFromViewModel = value;
                RaisePropertyChanged(nameof(InsertDataToDatabaseFromViewModel));
            }
        }

        public MainViewModel(InsertDataToDatabaseFormViewModel insertDataToDatabaseFormViewModel)
        {
            InsertDataToDatabaseFromViewModel = insertDataToDatabaseFormViewModel;
        }
    }
}
