using Microsoft.Data.SqlClient;
using ShopifyWebApi.Models;
using System.Data;

namespace ShopifyWebApi.Repository
{
    public class CategoryRepo
    {
        
        private SqlConnection conn;

        public void connection()
        {
            string conStr = "Server=localhost;Database=ShopifyWebAPIDB;Trusted_Connection=True;TrustServerCertificate=True;";
            conn = new SqlConnection(conStr);
        }
        public List<Category> getCategories()
        {
            connection();
            List<Category> categoriesList = new List<Category>();
            SqlCommand com = new SqlCommand("GetAllCategories", conn);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            conn.Open();
            da.Fill(dt);
            conn.Close();


            foreach (DataRow dr in dt.Rows)
            {
                categoriesList.Add(
                    new Category
                    {
                        categoryId = Convert.ToInt32(dr["CategoryId"]),
                        categoryName = Convert.ToString(dr["CategoryName"])
                    }

                );
            }
            return categoriesList;
        }

        public bool AddCategory(string categoryName)
        {
            connection();
            SqlCommand com = new SqlCommand("AddCategory", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@categoryName", categoryName);
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

        public bool UpdateCategory(Category obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateCategory", conn);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@categoryName", obj.categoryName);
            com.Parameters.AddWithValue("@categoryId", obj.categoryId);
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

        public bool DeleteCategory(int id)
        {
            connection();
            SqlCommand com = new SqlCommand("DeleteCategory", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CategoryId", id);
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

    }
}
