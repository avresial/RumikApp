using RumikApp.Enums;
using RumikApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RumikApp.Services
{
    public interface IFileDatabaseConnectionService : IDatabaseConnectionService
    {
        Settings ReadSettings();

        void SaveSettings(Settings newSettings);
    }
}