using RumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RumikApp.Services
{
    public interface IFileDatabaseConnectionService : IDatabaseConnectionService
    {
        bool CheckIsUserAbove18();
        void ChangeIsUserAbove18State(bool state);
    }
}