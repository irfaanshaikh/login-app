using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using login_app.Models;
using login_app.Operations;
using login_app.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace login_app.Controllers
{
    [Route("api/")]
    [ApiController]
    public class validateLoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public validateLoginController(IConfiguration configuration)
        {
            _config = configuration;
        }
    
        [HttpPost]
        [Route("getotp")]
        public IActionResult getOtp([FromBody] loginModels req)
        {
            response _response = response.GetInstance();
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.apiStatuscode = 400;
                    _response.error = $"Invalid request:{JsonConvert.SerializeObject(this.ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage))}";
                    _response.success = false;
                    _response.data = "";
                    _response.request = req;
                    _response.appErrorcode = 1001;
                    return StatusCode(StatusCodes.Status400BadRequest, _response);
                }

                getOTPOperations _getOTPOperations = new getOTPOperations();
                _response = _getOTPOperations.getOTP(req, _config);

            }
            catch (Exception ex)
            {
                _response.error = ex.Message;
            }

            return StatusCode(_response.apiStatuscode, _response);
        }

        [HttpPost]
        [Route("validateotp")]
        public IActionResult validateOtp([FromBody] validatOTP req)
        {
            response _response = response.GetInstance();
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.apiStatuscode = 400;
                    _response.error = $"Invalid request:{JsonConvert.SerializeObject(this.ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage))}";
                    _response.success = false;
                    _response.data = "";
                    _response.request = req;
                    _response.appErrorcode = 1002;
                    return StatusCode(StatusCodes.Status400BadRequest, _response);
                }

                validateOTPOperations _validateOTP = new validateOTPOperations();
                _validateOTP.validateoperations(req, _config);
            }
            catch (Exception ex)
            {
                _response.error = ex.Message;
            }

            return StatusCode(_response.apiStatuscode, _response);
        }

    }
}
