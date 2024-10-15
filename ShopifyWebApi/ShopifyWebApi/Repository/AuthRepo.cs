using Microsoft.Data.SqlClient;
using ShopifyWebApi.Models.Auth;

namespace ShopifyWebApi.Repository
{
    public class AuthRepo
    {

        private SqlConnection conn;

        public void connection()
        {
            string conStr = "Server=localhost;Database=ShopifyWebAPIDB;Trusted_Connection=True;TrustServerCertificate=True;";
            conn = new SqlConnection(conStr);
        }

        public bool isAdminAlredyRegistered(string email)
        {
            connection();
            SqlCommand cmd = new SqlCommand("getAdminEmail", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return reader.Read();
            }

        }

        public bool isUserAlredyRegistered(string email)
        {
            connection();
            SqlCommand cmd = new SqlCommand("getUserEmail", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return reader.Read();
            }
            
        }
        public bool addNewuser(User user) 
        {
            connection();
            
            SqlCommand cmd = new SqlCommand("registerUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", user.userName);
            cmd.Parameters.AddWithValue("@Email", user.userEmail);
            cmd.Parameters.AddWithValue("@Password", user.userPassword);
            conn.Open();
            int status = cmd.ExecuteNonQuery();
            conn.Close();
            if (status <= 0)
            {
                return false;
            }
            else
                return true;
            //conn.Close();
            
            
        }
        public bool addNewAdmin(Admin admin)
        {
            connection();

            SqlCommand cmd = new SqlCommand("registerAdmin", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AdminName", admin.adminName);
            cmd.Parameters.AddWithValue("@Email", admin.adminEmail);
            cmd.Parameters.AddWithValue("@Password", admin.adminPassword);
            conn.Open();
            int status = cmd.ExecuteNonQuery();
            conn.Close();
            if (status <= 0)
            {
                return false;
            }
            else
                return true;
            //conn.Close();


        }
        public bool loginUser(string email, string password)
        {
            connection();
            SqlCommand cmd = new SqlCommand("loginUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return reader.Read();
            }

        }

        public bool loginAdmin(string email, string password)
        {
            connection();
            SqlCommand cmd = new SqlCommand("loginAdmin", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                return reader.Read();
            }

        }
    }
}
