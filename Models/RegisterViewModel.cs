using System.ComponentModel.DataAnnotations;
namespace auction.Models
{
    public class RegisterViewModel 
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name - letters only, at least 2 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        [MinLength(2)]
        public string first_name { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name - letters only, at least 2 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters")]
        [MinLength(2)]
        public string last_name { get; set; }


        [Display(Name = "Username")]
        [Required(ErrorMessage = "Valid Email format")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username can only contain letters")]
        public string username { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password - at least 8 characters")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [Display(Name = "Confirm Password")]
        // [MinLength(8)]
        [Compare("password", ErrorMessage = "Password and confirmation must match")]
        [DataType(DataType.Password)]
        // [Required(ErrorMessage = "Password and confirmation must match")]
        public string password_confirmation { get; set; }
    }
}