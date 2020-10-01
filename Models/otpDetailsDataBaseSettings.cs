using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login_app.Models
{
    public class otpDetailsDataBaseSettings: IOtpDetailsDataBaseSettings
    {

        public otpDetailsDataBaseSettings setprops(IConfiguration _config)
        {
            otpDetailsDataBaseSettings _otpDetailsDataBaseSettings = new otpDetailsDataBaseSettings();
            _otpDetailsDataBaseSettings.collectionName = _config.GetValue<string>("appConfig:collectionName");
            _otpDetailsDataBaseSettings.databaseName = _config.GetValue<string>("appConfig:databaseName");
            _otpDetailsDataBaseSettings.ConnectionString = _config.GetValue<string>("appConfig:connectionString");
            return _otpDetailsDataBaseSettings;
        }
        public string collectionName { get; set; }
        public string ConnectionString { get; set; }
        public string databaseName { get; set; }
    }

    public interface IOtpDetailsDataBaseSettings
    {
         string collectionName { get; set; }
         string ConnectionString { get; set; }
         string databaseName { get; set; }
    }
}
