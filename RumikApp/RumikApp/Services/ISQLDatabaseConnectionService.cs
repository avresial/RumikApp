using RumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public interface ISQLDatabaseConnectionService : IDatabaseConnectionService
    {
        Task SendSearchingStatistics(Random random = null);
    }
}