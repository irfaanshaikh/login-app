using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace login_app.Models
{
    public class loginModels
    {
        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "mobileNumber not passed")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string mobileNumber { get; set; }
    }

    public class validatOTP
    {
        [Required(ErrorMessage = "mobileNumber not passed")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string mobileNumber { get; set; }
        [Required(ErrorMessage = "Otp not passed")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int OTP { get; set; }
    }
}
