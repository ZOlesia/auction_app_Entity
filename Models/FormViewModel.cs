using System.ComponentModel.DataAnnotations;
using System;
namespace auction.Models
{
    public class FormViewModel 
    {
        [Display(Name = "Poduct Name")]
        [Required(ErrorMessage = "Poduct Name - letters only, at least 3 characters")]
        [MinLength(3)]
        public string name { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description - letters only, at least 10 characters")]
        [MinLength(10)]
        public string description { get; set; }


        [Display(Name = "Starting Bid")]
        [Required(ErrorMessage = "Starting Bid is required")]
        public decimal bid { get; set; }


        [Display(Name = "End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime end_date { get; set; }

    }
}