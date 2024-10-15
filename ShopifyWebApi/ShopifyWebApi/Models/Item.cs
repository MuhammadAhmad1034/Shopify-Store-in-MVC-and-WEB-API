namespace ShopifyWebApi.Models
{
    public class Item
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        //public string unit { get; set; }
        public int subCategoryId { get; set; }
        //public int categoryId { get; set; }
        //public string author {  get; set; }


        public Item()
        {
            itemId = 0;
            //categoryId = 0;
            subCategoryId = 0;
        }

        public Item(int itemId, int categoryId, int subCategoryId, string itemName, int quantity, double price, string unit="",string optional="" )
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.quantity = quantity;
            this.price = price;
            //this.unit = unit;
            this.subCategoryId = subCategoryId;
            //this.categoryId = categoryId;
            //this.author = optional;
        }
    }
}
