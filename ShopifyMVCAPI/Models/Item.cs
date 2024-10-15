using System.ComponentModel.DataAnnotations;

namespace ShopifyMVCAPI.Models
{
    public class Item
    {
        [Display(Name = "Item Id")]
        public int itemId { get; set; }

        [Display(Name = "Item Name")]
        public string itemName { get; set; }
        
        [Display(Name = "Quantity Available")]

        public int quantity { get; set; }

        [Display(Name = "Price (Rs.)")]

        public double price { get; set; }

        
        [Display(Name = "Unit")]
        public string unit { get; set; }

        [Display(Name = "Sub Category Id")]
        public int subCategoryId { get; set; }
        
        [Display(Name = "Category Id")]
        public int categoryId { get; set; }

        [Display(Name = "Author")]
        public string author { get; set; }


        public Item()
        {
            itemId = 0;
            //categoryId = 0;
            subCategoryId = 0;
        }

        public Item(int itemId, int categoryId, int subCategoryId, string itemName, int quantity, double price, string unit = "", string optional = "")
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
