using System.ComponentModel.DataAnnotations;

namespace ShopifyMVCAPI.Models.Authentication
{
    public class Admin
    {
        [Display(Name = "Admin Id")]
        public int adminId { get; set; }

        [Display(Name = "Admin Name")]
        public string? adminName { get; set; }

        [Display(Name = "Password")]
        public string? adminPassword { get; set; }
        [Display(Name = "Email")]
        public string? adminEmail { get; set; }
        public Admin()
        {
            adminId = 0;
            adminName= null;
        }
    }
}
