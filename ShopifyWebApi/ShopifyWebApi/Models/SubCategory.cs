namespace ShopifyWebApi.Models
{
    public class SubCategory
    {
        public int subCategoryId { get; set; }
        public string subCategoryName { get; set; }
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
