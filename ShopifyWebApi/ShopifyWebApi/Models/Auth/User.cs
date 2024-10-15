using System.ComponentModel.DataAnnotations;

namespace ShopifyWebApi.Models.Auth
{
    public class User
    {
        
        public int userId { get; set; }

        [Display(Name = "User Name")]
        public string? userName { get; set; }

        [Display(Name = "User Password")]

        public string? userPassword { get; set; }

        

        public string? userEmail { get; set; }

        public User()
        {
            userId = 0;
        }
    }
}
