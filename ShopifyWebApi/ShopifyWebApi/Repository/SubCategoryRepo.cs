using Microsoft.Data.SqlClient;
using ShopifyWebApi.Models;
using System.Data;

namespace ShopifyWebApi.Repository
{
    public class SubCategoryRepo
    {
        //public Dictionary<int, List<string>> subCategories = new Dictionary<int, List<string>>();
        //public List<SubCategory> subCategories { get; set; }

        private SqlConnection conn;

        public void connection()
        {
            string conStr = "Server=localhost;Database=ShopifyWebAPIDB;Trusted_Connection=True;TrustServerCertificate=True;";
            conn = new SqlConnection(conStr);
        }
        //public SubCategoryRepo() {
            
        //    //subCategories = new List<SubCategory>();
        //    //subCategories.Add(new SubCategory(0, "Vegetables",1));
        //    //subCategories.Add(new SubCategory(1, "Fruits", 1));
        //    //subCategories.Add(new SubCategory(2, "Dairy Products", 1));
        //    //subCategories.Add(new SubCategory(0, "Mobile", 3));
        //    //subCategories.Add(new SubCategory(1, "Wearables",3));
        //    //subCategories.Add(new SubCategory(2, "TVs", 3));
        //    //subCategories.Add(new SubCategory(0, "History", 2));
        //    //subCategories.Add(new SubCategory(1, "Philosophy", 2));
        //    //subCategories.Add(new SubCategory(2, "Politics", 2));
        //    //subCategories.Add(++i, new List<string> { "Vegetables", "Fruits", "Dairy Products" });
        //    //subCategories.Add(++i, new List<string> { "Mobile", "Wearables", "TVs" });
        //    //subCategories.Add(++i, new List<string> { "History", "Politics", "Philosophy" });

        //}

        public List<SubCategory> getSubCategories(int categoryId)
        {

            connection();
            List<SubCategory> subCategoriesList = new List<SubCategory>();
            SqlCommand com = new SqlCommand("GetAllSubCategories", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CategoryId", categoryId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();


            foreach (DataRow dr in dt.Rows)
            {

                subCategoriesList.Add(
                    new SubCategory
                    {
                        subCategoryId = Convert.ToInt32(dr["SubCategoryId"]),
                        categoryId = Convert.ToInt32(dr["CategoryId"]),
                        subCategoryName = Convert.ToString(dr["SubCategoryName"])

                    }

                );
            }
            return subCategoriesList;
        }



        public bool AddSubCategory(string subCategoryName, int categoryId)
        {
            connection();
            SqlCommand com = new SqlCommand("AddSubCategory", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SubCategoryName", subCategoryName);
            com.Parameters.AddWithValue("@CategoryId", categoryId);

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

        public bool UpdateSubCategory(SubCategory obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateSubCategory", conn);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SubCategoryName", obj.subCategoryName);
            com.Parameters.AddWithValue("@CategoryId", obj.categoryId);
            com.Parameters.AddWithValue("@SubCategoryId", obj.subCategoryId);
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

        public bool DeleteSubCategory(int id)
        {
            connection();
            SqlCommand com = new SqlCommand("DeleteSubCategory", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SubCategoryId", id);
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
        public SubCategory GetSubCategorybyID(int id)
        {

            connection();
            SubCategory subCat = null;
            SqlCommand com = new SqlCommand("GetSubCategoryItemById", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SubCategoryId", id);
            conn.Open();
            using (SqlDataReader reader = com.ExecuteReader())
            {
                while(reader.Read())
                {
                    subCat = new SubCategory{ 
                        categoryId = Convert.ToInt32(reader["CategoryId"]),
                        subCategoryName= Convert.ToString(reader["SubCategoryName"]),
                        subCategoryId = Convert.ToInt32(reader["SubCategoryId"])};
                }
            }
            conn.Close();
            return subCat;
            
        }
    }
}
