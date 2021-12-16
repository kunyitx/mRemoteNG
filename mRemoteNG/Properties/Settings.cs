using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRemoteNG.Properties
{
    internal class Settings
    {
        public Settings()
        {
            var configuration = new Configuration("AppSettings");
            var appSettings = configuration.Get("AppSettings"); // null
            var token = configuration.Get("token"); // null
        }
    }
}
