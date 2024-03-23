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

        public Employee GetUserByEmail(string email)
        {
            Employee employee = new Employee();
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
                    employee.Email = (string)UserReader["Email"];
                    employee.Password = (string)UserReader["Password"];
                    employee.UserSalt = (string)UserReader["UserSalt"];
                    employee.Role = (string)UserReader["Role"];
                    //user.AccountStatus = (int)UserReader["DeactivateAccountStatus"];
                    employee.DateJoined = (DateTime)UserReader["DateOfCreation"];
                }
            }
            UserReader.Close();
            cnMaisonsConnection.Close();
            return employee;
        } 

        public bool AddEmployee (Employee employee)
        {
            bool success = true;
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            SqlCommand AddEmployeeCommand = new()
            {
                Connection = cnMaisonsConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddEmployee",
            };


            SqlParameter FirstNameParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.FirstName,
                Direction = ParameterDirection.Input,
            };
            SqlParameter LastNameParamter = new()
            {
                ParameterName = "@Lastname",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.LastName,
                Direction = ParameterDirection.Input,
            };
            SqlParameter PhoneNumberParamter = new()
            {
                ParameterName = "@PhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.PhoneNumber,
                Direction = ParameterDirection.Input,
            };
            SqlParameter EmailParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.Email,
                Direction = ParameterDirection.Input,
            };
            SqlParameter PasswordParameter = new()
            {
                ParameterName = "@Password",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.Password,
                Direction = ParameterDirection.Input,
            };
            SqlParameter RoleParameter = new()
            {
                ParameterName = "@Role",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.Role,
                Direction = ParameterDirection.Input,
            };
            SqlParameter DefaultPassword = new()
            {
                ParameterName = "@DefaultPassword ",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = employee.Password,
                Direction = ParameterDirection.Input,
            };
            SqlParameter UserSalt = new()
            {
                ParameterName = "@UserSalt",
                SqlValue = employee.UserSalt,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
            };

            try
            {
                AddEmployeeCommand.Parameters.Add(FirstNameParameter);
                AddEmployeeCommand.Parameters.Add(LastNameParamter);
                AddEmployeeCommand.Parameters.Add(PhoneNumberParamter); 
                AddEmployeeCommand.Parameters.Add(EmailParameter);
                AddEmployeeCommand.Parameters.Add(PasswordParameter);
                AddEmployeeCommand.Parameters.Add(RoleParameter);
                AddEmployeeCommand.Parameters.Add(DefaultPassword);
                AddEmployeeCommand.Parameters.Add(UserSalt);                

                AddEmployeeCommand.ExecuteNonQuery();
                cnMaisonsConnection.Close();
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }
    }
}
