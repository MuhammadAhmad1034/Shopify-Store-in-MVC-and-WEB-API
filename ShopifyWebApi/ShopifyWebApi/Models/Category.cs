namespace ShopifyWebApi.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }

        public Category()
        {
            categoryId = 0; 
            categoryName = string.Empty;
        }
        public Category(int categoryId, string categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }
    }
}
