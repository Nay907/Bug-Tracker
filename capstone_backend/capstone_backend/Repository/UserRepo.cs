using capstone_backend.Models;
using capstone_backend.Repository.interfaces;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Text;

namespace capstone_backend.Repository
{
    public class UserRepo : IUserRepo
    {
        private string connectionString = "Data Source=APINP-ELPTF0UAL\\SQLEXPRESS;Initial Catalog=capstone_database;Persist Security Info=True;User ID=tap2023;Password=tap2023;Encrypt=False";

        public List<User> GetAllUsers()
        {
            List<User> User = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                EmailId = reader["EmailId"].ToString(),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                Access = reader["Access"].ToString()
                            };
                            User.Add(user);
                        }
                    }
                }
            }
            return User;
        }

        public User GetUserById(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM User WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                
                                Password = reader["Password"].ToString(),
                                EmailId = reader["EmailId"].ToString(),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                Access = reader["Access"].ToString()
                            };
                        }
                    }
                }
            }
            return user;
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Username, EmailId, Password, RoleId, Access) VALUES (@Username, @EmailId, @Password, @RoleId, @Access)", connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@EmailId", user.EmailId);
                    
                    command.Parameters.AddWithValue("@RoleId", user.RoleId);
                    command.Parameters.AddWithValue("@Access", user.Access);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE User SET UserId= @UserId, Username = @Username, EmailId = @EmailId, Password = @Password, RoleId = @RoleId, Access = @Access WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@EmailId", user.EmailId);
                    
                    command.Parameters.AddWithValue("@RoleId", user.RoleId);
                    command.Parameters.AddWithValue("@Access", user.Access);
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public string LoginUser(User userInput)        // check if user exists and return required
        {
            List<User> User = new List<User>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd;
                SqlDataReader rdr;

                string query = $"SELECT * FROM Users";
                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User user = new User();
                    user.UserId = Int32.Parse(rdr["UserID"].ToString());
                    user.Access = rdr["Access"].ToString();
                    user.Username = rdr["Username"].ToString();
                    user.EmailId = rdr["EmailId"].ToString();
                    user.Password = rdr["Password"].ToString();

                    User.Add(user);
                }
                con.Close();

                foreach (var x in User)
                {
                    Console.WriteLine(x);
                    if (x.EmailId == userInput.EmailId)
                    {
                        if (x.Password == userInput.Password)
                        {
                            return "Logged In!";
                        }
                        else
                        {
                            return "Wrong Password";
                        }
                    }
                }
                return "Email ID Not Registered... Try Again!";
            }
        }
    }
}
