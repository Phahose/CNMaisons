using CNMaisons.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace CNMaisons.TechnicalService
{
    public class Users
    {
        private string? connectionString;
        public Users()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }

        public User GetUserByEmail(string email)
        {
            User user = new User();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            SqlCommand GetUser = new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = cnMaisonsConnection,
                CommandText = "GetUserByEmail"
            };

            SqlParameter EmailParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = email
            };

            GetUser.Parameters.Add(EmailParameter);
            SqlDataReader UserReader = GetUser.ExecuteReader();

            if (UserReader.HasRows)
            {
                while (UserReader.Read())
                {
                    user.Email = (string)UserReader["Email"];
                    user.Password = (string)UserReader["Password"];
                    user.UserSalt = (string)UserReader["UserSalt"];
                    user.Role = (string)UserReader["Role"];
                    //user.AccountStatus = (int)UserReader["DeactivateAccountStatus"];
                    //user.DateJoined = (DateTime)UserReader["DateOfCreation"];
                }
            }
            UserReader.Close();
            cnMaisonsConnection.Close();
            return user;
        }






        public String AddUser(User user)
        {
            String Success;
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            try
            {
                // Connection
                SqlConnection MyDataSource = new SqlConnection();
                MyDataSource.ConnectionString = connectionString;
                MyDataSource.Open();

                // Command
                SqlCommand MyCommand = new SqlCommand();
                MyCommand.Connection = MyDataSource;
                MyCommand.CommandType = CommandType.StoredProcedure;
                MyCommand.CommandText = "AddUser";

                void AddParameter(string parameterName, SqlDbType sqlDbType, object value)
                {
                    MyCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = parameterName,
                        SqlDbType = sqlDbType,
                        Direction = ParameterDirection.Input,
                        Value = value
                    });
                }

                byte[] salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                byte[] hashedPassword = HashPasswordWithSalt(user.Password, salt);

                // Convert the salt and hashed password to Base64 for storage
                string saltBase64 = Convert.ToBase64String(salt);
                string hashedPasswordBase64 = Convert.ToBase64String(hashedPassword);

                // Adding parameters
                AddParameter("@FirstName", SqlDbType.VarChar, user.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, user.LastName);
                AddParameter("@Email", SqlDbType.VarChar, user.Email);
                AddParameter("@Password", SqlDbType.VarChar, hashedPasswordBase64);
                AddParameter("@Role", SqlDbType.VarChar, user.Role);
                AddParameter("@DefaultPassword", SqlDbType.VarChar, user.DefaultPassword);
                AddParameter("@UserSalt", SqlDbType.VarChar, saltBase64);

                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                Success = "Successful!";
            }
            catch (SqlException ex)
            {

                if (ex.Number == 2627) // Unique constraint violation error number
                {

                    Success = "This Tenant ID or the Email already exist. ";
                }
                else
                {
                    Success = $"An error occurred: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                Success = $"An error occurred: {ex.Message}";
            }
            return Success;

        }
        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            // Hash the password with PBKDF2 using HMACSHA256
            return new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256).GetBytes(32);
        }

    }
}
