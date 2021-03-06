using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooNotesModelLayer
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Enter Your Mail ID")]
        [RegularExpression(@"^[1-9A-Za-z]+[.][a-zA-Z]*@(bl)[.](co)([.](in))?$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!_%*#?&]{1})[a-zA-Z0-9@$!_%*#?&]{8,}$", ErrorMessage = "Not a Password")]
        public string Password { get; set; }
    }
}
