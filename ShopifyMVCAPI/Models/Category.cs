using System.ComponentModel.DataAnnotations;

namespace ShopifyMVCAPI.Models
{
    public class Category
    {
        [Display(Name = "Category Id")]
        public int categoryId { get; set; }
        [Display(Name = "Category Name")]

        public string categoryName { get; set; }
    }
}
