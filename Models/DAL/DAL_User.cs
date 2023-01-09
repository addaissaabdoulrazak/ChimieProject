
using ChimieProject.Models.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ChimieProject.Models.DAL
{
    public class DAL_User
    {
        public static int Add(User user)
        {
            try
            {
                //code here
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();  
                    string StrSQL = "INSERT INTO [User] (Username, PasswordHash,ConfirmPassword,Email,Role)  VALUES (@Username, @PasswordHash,@ConfirmPassword,@Email,@Role);";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.AddWithValue("@Username", user.Username ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Role", user.Role ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            
        }
      
        public static void Delete(int Id)
        {

            try
            {

                //code here
                using (SqlConnection connection = DBConnection.GetConnection())
                {

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static User GetUser(int Id)
        {
            User user = new User();

            try
            {
                //code here
                using (SqlConnection connection = DBConnection.GetConnection())
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return user;
        }

        //
        public static User GetUserByName(string userName)
        {

           

            try
            {
                //code here
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    User user = new User();

                    connection.Open();

                string StrSQL = "SELECT * FROM [User] WHERE Username = @userName;";

                SqlCommand command = new SqlCommand(StrSQL, connection);

                command.Parameters.AddWithValue("@userName", userName);
                
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                           

                            user.Id = Convert.ToInt32(dataReader["Id"]);

                            user.Username=Convert.ToString( dataReader["Username"]);

                            user.PasswordHash = Convert.ToString(dataReader["PasswordHash"]); 

                            user.Email = dataReader["Email"].ToString();

                            user.Role = dataReader["Role"].ToString();

                            return user;
                        }
                       
                        connection.Close();

                        
                    }

                   
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

             
            }

            return null;
        }

        //

        //Get User By Email

        public static User GetUserByEmail(string Email)
        {



            try
            {
                //code here
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    User user = new User();

                    connection.Open();

                    string StrSQL = "SELECT * FROM [User] WHERE Email = @Email;";

                    SqlCommand command = new SqlCommand(StrSQL, connection);

                    command.Parameters.AddWithValue("@Email", Email);

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {


                            user.Id = Convert.ToInt32(dataReader["Id"]);

                            user.Username = Convert.ToString(dataReader["Username"]);

                            user.PasswordHash = Convert.ToString(dataReader["PasswordHash"]);

                            user.Email = dataReader["Email"].ToString();

                            user.Role = dataReader["Role"].ToString();

                            return user;
                        }

                        connection.Close();


                    }


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);


            }

            return null;
        }

        //End
        public static List<User> GetAll()
        {
            List<User> listUser = new List<User>();

            try
            {
                //code here
                using (SqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string StrSQL = "SELECT * FROM dbo.User";
                    SqlCommand command = new SqlCommand(StrSQL, connection);

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {
                            User user = new User();
                            user.Id = (int)dataReader["id"];
                              
                            //
                               //---------------------En cours -------------------------------
                            //
                            
                            listUser.Add(user);
                        }

                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listUser;
        }

    }
}
