using CNMaisons.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
namespace CNMaisons.TechnicalService
{
    public class UsersManager
    {
        private string? connectionString;
        public UsersManager()
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
                    user.DateJoined = (DateTime)UserReader["DateOfCreation"];
                }
            }
            UserReader.Close();
            cnMaisonsConnection.Close();
            return user;
        } 

        public bool AddUser (User user)
        {
            bool success = false;
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            SqlCommand AddUserCommand = new()
            {
                Connection = cnMaisonsConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddUser",
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
                SqlValue = user.Password,
                Direction = ParameterDirection.Input,
            };
            SqlParameter RoleParameter = new()
            {
                ParameterName = "@Role",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = user.Role,
                Direction = ParameterDirection.Input,
            };
            SqlParameter DefaultPassword = new()
            {
                ParameterName = "@DefaultPassword ",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = user.Password,
                Direction = ParameterDirection.Input,
            };
            SqlParameter UserSalt = new()
            {
                ParameterName = "@UserSalt",
                SqlValue = user.UserSalt,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
            };

            try
            {
                AddUserCommand.Parameters.Add(EmailParameter);
                AddUserCommand.Parameters.Add(PasswordParameter);
                AddUserCommand.Parameters.Add(RoleParameter);
                AddUserCommand.Parameters.Add(DefaultPassword);
                AddUserCommand.Parameters.Add(UserSalt);                

                AddUserCommand.ExecuteNonQuery();
                cnMaisonsConnection.Close();
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }
    }
}
