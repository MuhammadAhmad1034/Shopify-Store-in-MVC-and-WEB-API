using Microsoft.Data.SqlClient;
using ShopifyWebApi.Models;
using System.Data;
namespace ShopifyWebApi.Repository
{
    

    public class ItemRepo
    {
        public List<List<object>> bookItems;
        public List<List<object>> electronicsItems;
        public List<List<object>> groceryItems;
        private SqlConnection conn;
        public void connection()
        {
            string conStr = "Server=localhost;Database=ShopifyWebAPIDB;Trusted_Connection=True;TrustServerCertificate=True;";
            conn = new SqlConnection(conStr);
        }
        
        public ItemRepo()
        {
            //int i = 0;
            //items = new List<Item>
            //{
            //    new Item(++i, 1,0, "Potatoes", 12, 100, "kg" ),
            //    new Item( ++i,1,0, "Tomatoes", 15, 140, "kg" ),
            //    new Item(++i, 1,0, "Onions", 30, 170, "kg" ),
            //    new Item( ++i, 1,0, "Cucumber", 5, 30, "kg" ),
            //    new Item(  ++i,1,1, "Apple", 10, 300, "kg" ),
            //    new Item(  ++i,1,1, "Grapes", 8, 250, "kg" ),
            //    new Item(  ++i,1,1, "Strawberry", 8, 450, "kg" ),
            //    new Item(  ++i,1,1, "Apricot", 4, 350, "kg" ),
            //    new Item(  ++i,1,2, "Milk", 40, 300, "ltr" ),
            //    new Item(  ++i,1,2, "Yougurt", 10, 350, "ltr" ),
            //    new Item(  ++i,1,2, "Cheese", 15, 650, "kg" ),

            //    new Item(++i, 2,  0, "The Siege", 1, 3595.50,"","Ben Macintyre" ),
            //    new Item(++i, 2, 0, "The Land of Hope and Fear", 11, 6295.00,"","Isabel Kershner" ),
            //    new Item( ++i,2, 0, "Influence",19 , 3659.00,"","Justin Jones" ),
            //    new Item(++i, 2, 0, "How the World Works", 22, 5499.00,"","Noam Chomsky" ),
            //    new Item(++i, 2, 2, "Influence", 19, 3659.00,"","Justin Jones" ),
            //    new Item(++i, 2, 0, "How the World Works", 22, 5499.00,"","Noam Chomsky" ),
            //    new Item(++i, 2, 2, "Humanly Possible",22, 3895.00, "","Sarah Bakewell" ),
            //    new Item(++i, 2, 1, "Silencing Kashmir",9 , 1899.00,"","Anees Jilani" ),
            //    new Item(++i, 2, 2, "The Land of Hope and Fear",11 , 6295.00,"","Isabel Kershner" ),
            //    new Item(++i, 2, 1, "Crisis of Conscience",1 , 1796.00,"","Tom Mueller" ),
            //    new Item(++i, 2, 1, "The Siege", 3, 3595.50,"","Ben Macintyre"),
            //    new Item(++i, 2, 1, "The Land of Hope and Fear",9 , 6295.00,"","Isabel Kershner" ),

            //    new Item (++i, 3, 0, "Samsung Galaxy A32", 51, 32000),
            //    new Item (++i, 3, 0, "Samsung Galaxy A52", 12, 56000 ),
            //    new Item (++i, 3, 0, "Samsung Galaxy Z Flip 5", 5, 309999 ),
            //    new Item (++i, 3, 0, "Samsung Galaxy Z Fold 5", 10, 504999 ),
            //    new Item (++i, 3, 0, "Samsung Galaxy S22", 12, 424999 ),
            //    new Item (++i, 3, 1, "Apple Watch Ultra", 8, 184999 ),
            //    new Item ( ++i, 3,1, "Samsung Galaxy Watch", 10, 52499 ),
            //    new Item (++i, 3, 1, "Huawei Band 7 Watch", 41, 16972 ),
            //    new Item (++i, 3, 1, "Haylou GS Lite Watch", 20, 7649 ),
            //    new Item (++i, 3, 2, "Samsung 43 Inch UHD 4K", 28, 124999),
            //    new Item (++i, 3, 2, "Sony 65 Inch Bravia 4K", 12, 319999 ),
            //    new Item (++i, 3, 2, "LG 65Inch Smart UHD 4K", 10, 280527 )
            //    };
            
            
        }

        public List<Item> getItems(int id,int id2)
        {

            connection();
            List<Item> items = new List<Item>();
            SqlCommand com = new SqlCommand("GetAllItems", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SubCategoryId", id2);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();


            foreach (DataRow dr in dt.Rows)
            {

                items.Add(
                    new Item
                    {
                        itemId = Convert.ToInt32(dr["ItemId"]),
                        quantity = Convert.ToInt32(dr["ItemQuantity"]),
                        itemName = Convert.ToString(dr["ItemName"]),
                        price = Convert.ToDouble(dr["ItemPrice"]),
                        subCategoryId = Convert.ToInt32(dr["SubCategoryId"]) 


                    }

                );
            }
            
            return items;
        }

        public bool AddItem(string itemName, int quantity,float price,int subCategoryId)
        {
            connection();
            SqlCommand com = new SqlCommand("AddItem", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ItemName", itemName);
            com.Parameters.AddWithValue("@ItemPrice", price);
            com.Parameters.AddWithValue("@ItemQuantity", quantity);
            com.Parameters.AddWithValue("@SubCategoryId", subCategoryId);
            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateItem(Item obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateItem", conn);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ItemName", obj.itemName);
            com.Parameters.AddWithValue("@ItemId", obj.itemId);
            com.Parameters.AddWithValue("@SubCategoryId", obj.subCategoryId);
            com.Parameters.AddWithValue("@ItemQuantity", obj.quantity);
            com.Parameters.AddWithValue("@ItemPrice", obj.price);


            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteItem(int id)
        {
            connection();
            SqlCommand com = new SqlCommand("DeleteItem", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ItemId", id);
            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Item GetItemById(int id)
        {
            connection();
            Item item = null;
            SqlCommand com = new SqlCommand("GetItemById", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ItemId", id);
            conn.Open();
            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    item = new Item
                    {
                        quantity = Convert.ToInt32(reader["ItemQuantity"]),
                        itemName = Convert.ToString(reader["ItemName"]),
                        subCategoryId = Convert.ToInt32(reader["SubCategoryId"]),
                        price = Convert.ToDouble(reader["ItemPrice"]),
                        itemId = Convert.ToInt32(reader["ItemId"])
                    };
                }
            }
            conn.Close();
            return item;

        }

    }
}
