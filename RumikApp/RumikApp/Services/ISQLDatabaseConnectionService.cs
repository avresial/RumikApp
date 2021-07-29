using RumikApp.Enums;
using RumikApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public interface ISQLDatabaseConnectionService : IDatabaseConnectionService
    {
        Task SendSearchingStatistics(PollData pollData);
    }
}