using login_app.Models;
using login_app.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;

namespace login_app.Operations
{
    public class getOTPOperations
    {
        
        public response getOTP(loginModels req, IConfiguration _config)
        {
            response _response = response.GetInstance();
            string token = string.Empty;
            try
            {
                Random _rdm = new Random();
                int num = _rdm.Next(1000, 9999);
                otpDetails _otpDetails = new otpDetails();
                _otpDetails.mobileNumber = req.mobileNumber;
                _otpDetails.otp = num;


                //send otp using sms
                token = _config.GetValue<string>("token:value");
                if (!sendSMS(req.mobileNumber,num, token))
                {
                    _response.apiStatuscode = 500;
                    _response.error = $"Error while Sending SMS OTP";
                    _response.success = false;
                    _response.data = "";
                    _response.request = req;
                    _response.appErrorcode = 1003;
                    return _response;
                }
                otpDetailsDataBaseSettings _otpDetailsDataBaseSettings = new otpDetailsDataBaseSettings();
                _otpDetailsDataBaseSettings = _otpDetailsDataBaseSettings.setprops(_config);
                otpService _otpService = new otpService(_otpDetailsDataBaseSettings);
                otpDetails _otpDetailsdata=_otpService.Get(req.mobileNumber);

                if (_otpDetailsdata == null)
                {
                    _otpDetails = _otpService.Create(_otpDetails);
                }
                else
                {
                    _otpDetails._id = _otpDetailsdata._id;
                    _otpService.Update(_otpDetailsdata._id, _otpDetails);
                }

                _response.data = $"OTP sent on Mobile:{_otpDetails.mobileNumber}";
                _response.apiStatuscode = 200;
                _response.appErrorcode = 0;
                _response.success = true;
                _response.error = "";
                _response.request = req;
            }
            catch (Exception ex)
            {
                _response.error = ex.Message;
                
            }
            return _response;
        }

        public bool sendSMS(string mobileNumber,int otp,string token)
        {
            #region declaration
            String result;
            string message = "Hi User,This is your OTP:"+ otp;
            string sender = "TXTLCL";
            #endregion
            try
            {
                String url = "https://api.textlocal.in/send/?apikey=" + token + "&numbers=" + mobileNumber + "&message=" + message + "&sender=" + sender;

                StreamWriter myWriter = null;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                objRequest.Method = "POST";
                objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                objRequest.ContentType = "application/x-www-form-urlencoded";
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
                myWriter.Close();
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    // Close and clean up the StreamReader
                    sr.Close();
                }

            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
