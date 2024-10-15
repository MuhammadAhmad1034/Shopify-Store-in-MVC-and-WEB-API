using System.ComponentModel.DataAnnotations;

namespace ShopifyMVCAPI.Models.Authentication
{
    public class User
    {
        [Display(Name = "User Id")]
        public int userId { get; set; }

        [Display(Name = "User Name")]
        
        public string? userName { get; set; }

        [Display(Name = "User Password")]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password length must be atleast 8")]
        
        public string? userPassword { get; set; }
        
        [Display(Name = "User Email")]
        [Required(ErrorMessage = "Email is required.")]
        public string? userEmail { get; set; }

        public User()
        {
            userId = 0;
        }
    }
}
