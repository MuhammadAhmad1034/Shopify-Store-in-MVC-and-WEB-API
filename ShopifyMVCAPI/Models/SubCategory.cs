using System.ComponentModel.DataAnnotations;

namespace ShopifyMVCAPI.Models
{
    public class SubCategory
    {
        [Display(Name = "SubCategory Id")]
        public int subCategoryId { get; set; }

        [Display(Name = "SubCategory Name")]

        public string subCategoryName { get; set; }

        [Display(Name = "Category Id")]

        public int categoryId { get; set; }

        public SubCategory()
        {
            subCategoryId = 0;
            categoryId = 0;
        }
        public SubCategory(int subCategoryId, string subCategoryName, int categoryId)
        {
            this.subCategoryId = subCategoryId;
            this.subCategoryName = subCategoryName;
            this.categoryId = categoryId;
        }
    }
}
