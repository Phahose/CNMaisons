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
                    user.DefaultPassword = (string)UserReader["DefaultPassword"];
                    user.DeactivateAccountStatus = (bool)UserReader["DeactivateAccountStatus"];
                }
            }
            UserReader.Close();
            cnMaisonsConnection.Close();
            return user;
        }

        public bool AddUser(User user)
        {
            bool Success = false;
            string successMessage;
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
                AddParameter("@Email", SqlDbType.VarChar, user.Email);
                AddParameter("@Password", SqlDbType.VarChar, hashedPasswordBase64);
                AddParameter("@Role", SqlDbType.VarChar, user.Role);
                AddParameter("@DefaultPassword", SqlDbType.VarChar, user.DefaultPassword);
                AddParameter("@UserSalt", SqlDbType.VarChar, saltBase64);

                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                successMessage = "Successful!";
                Success = true;
            }
            catch (SqlException ex)
            {

                if (ex.Number == 2627) // Unique constraint violation error number
                {

                    successMessage = "This Tenant ID or the Email already exist. ";
                }
                else
                {
                    successMessage = $"An error occurred: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                successMessage = $"An error occurred: {ex.Message}";
            }
            return Success;

        }

        public bool UpdateUser(User user)
        {
            bool success = true;
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            try
            {
                byte[] salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                byte[] hashedPassword = HashPasswordWithSalt(user.Password, salt);
                // Convert the salt and hashed password to Base64 for storage
                string saltBase64 = Convert.ToBase64String(salt);
                string hashedPasswordBase64 = Convert.ToBase64String(hashedPassword);

                SqlCommand UpdateUserCommand = new()
                {
                    CommandType = CommandType.StoredProcedure,
                    Connection = cnMaisonsConnection,
                    CommandText = "UpdateUsers"
                };

                SqlParameter EmailParameter = new()
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = user.Email,
                    Direction = ParameterDirection.Input,
                };
                SqlParameter PasswordParameter = new()
                {
                    ParameterName = "@Password",
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = hashedPasswordBase64,
                    Direction = ParameterDirection.Input,
                };
                SqlParameter RoleParameter = new()
                {
                    ParameterName = "@Role",
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = user.Role,
                    Direction = ParameterDirection.Input,
                };
                SqlParameter DeactivateParameter = new()
                {
                    ParameterName = "@DeactivateAccountStatus",
                    SqlDbType = SqlDbType.Bit,
                    SqlValue = user.DeactivateAccountStatus,
                    Direction = ParameterDirection.Input,
                };
                SqlParameter DefaultPasswordParameter = new()
                {
                    ParameterName = "@DefaultPassword",
                    SqlDbType = SqlDbType.NVarChar,
                    SqlValue = user.DefaultPassword,
                    Direction = ParameterDirection.Input,
                };
                SqlParameter UserSaltParameter = new()
                {
                    ParameterName = "@UserSalt",
                    SqlDbType = SqlDbType.NVarChar,
                    SqlValue = saltBase64,
                    Direction = ParameterDirection.Input,
                };

                UpdateUserCommand.Parameters.Add(EmailParameter);
                UpdateUserCommand.Parameters.Add(PasswordParameter);
                UpdateUserCommand.Parameters.Add(RoleParameter);
                UpdateUserCommand.Parameters.Add(DeactivateParameter);
                UpdateUserCommand.Parameters.Add(DefaultPasswordParameter);
                UpdateUserCommand.Parameters.Add(UserSaltParameter);

                UpdateUserCommand.ExecuteNonQuery();

                cnMaisonsConnection.Close();
            }
            catch (Exception)
            {
                success = false;
                throw;
            }

            return success;
        }
        
        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            // Hash the password with PBKDF2 using HMACSHA256
            return new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256).GetBytes(32);
        }


        public bool CheckIfEmailAlreadyExistInUsersTable(string email)
        {
            //connection
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = connectionString;
            MyDataSource.Open();

            //Command
            SqlCommand MyCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "CheckIfEmailAlreadyExistInUsersTable"
            };

            SqlParameter MyParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = email,
                Direction = ParameterDirection.Input,
            };
            MyCommand.Parameters.Add(MyParameter);

            SqlDataReader MyDataReader = MyCommand.ExecuteReader();
            bool Success = false;

            if (MyDataReader.HasRows)
            {
                Success = true;
            }
            return Success;
        }
        public List<User> GetUsers()
        {
            List<User> users = new();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            SqlCommand GetAllUsers = new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = cnMaisonsConnection,
                CommandText = "GetAllUsers"
            };

            SqlDataReader UserReader = GetAllUsers.ExecuteReader();

            if (UserReader.HasRows)
            {
                while (UserReader.Read())
                {
                    User user = new User();
                    user.Email = (string)UserReader["Email"];
                    user.Password = (string)UserReader["Password"];
                    user.UserSalt = (string)UserReader["UserSalt"];
                    user.Role = (string)UserReader["Role"];
                    user.DefaultPassword = (string)UserReader["DefaultPassword"];
                    user.DeactivateAccountStatus = (bool)UserReader["DeactivateAccountStatus"];

                    users.Add(user);
                }
            }
            UserReader.Close();
            cnMaisonsConnection.Close();
            return users;
        }
    }
}
