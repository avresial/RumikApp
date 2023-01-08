using RumikApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public interface ISettingsService
    {
        Settings ReadSettings();

        void SaveSettings(Settings newSettings);
    }
}
