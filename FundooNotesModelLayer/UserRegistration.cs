using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundooNotesModelLayer
{
    public class UserRegistration
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        [Required(ErrorMessage = "Enter Your FirstName")]
        [StringLength(15, ErrorMessage = "FirstName should be less than or equal to Fifteen characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Your LastName")]
        [StringLength(50, ErrorMessage = "LastName should be less than or equal to fifty characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Your must provide a Mobile Number")]
        [RegularExpression(@"^(91)[ ][789][0-9]{9}$", ErrorMessage = "Not a valid Mobile number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter Your Mail ID")]
        [RegularExpression(@"^[1-9A-Za-z]+[.][a-zA-Z]*@(bl)[.](co)([.](in))?$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!_%*#?&]{1})[a-zA-Z0-9@$!_%*#?&]{8,}$", ErrorMessage = "Not a Password")]
        public string Password { get; set; }
    }
}
