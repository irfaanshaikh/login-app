using login_app.Models;
using login_app.Utility;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login_app.Operations
{
    public class validateOTPOperations
    {
        public response validateoperations(validatOTP req, IConfiguration _config)
        {
            response _response = response.GetInstance();

            try
            {
                otpDetailsDataBaseSettings _otpDetailsDataBaseSettings = new otpDetailsDataBaseSettings();
                _otpDetailsDataBaseSettings= _otpDetailsDataBaseSettings.setprops(_config);
                otpService _otpService = new otpService(_otpDetailsDataBaseSettings);
                otpDetails _otpDetails = _otpService.Get(req.mobileNumber);
                if (_otpDetails.otp == req.OTP && _otpDetails.mobileNumber == req.mobileNumber)
                {
                    _response.apiStatuscode = 200;
                    _response.error = "";
                    _response.success = true;
                    _response.data = "Valid OTP";
                    _response.request = req;
                    _response.appErrorcode = 0;
                    
                }
                else 
                {
                    _response.apiStatuscode = 404;
                    _response.error = "Inavlid OTP";
                    _response.success = true;
                    _response.data = "";
                    _response.request = req;
                    _response.appErrorcode = 0;

                }
            }
            catch (Exception ex)
            {
                _response.error = ex.Message;

            }

            return _response;
        }
    }
}
