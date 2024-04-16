using CNMaisons.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace CNMaisons.TechnicalService
{
    public class Employees
    {
        private string? connectionString;
        public Employees()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }

        public bool AddEmployee(User user, Employee employee)
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
                MyCommand.CommandText = "AddEmployee";

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
                AddParameter("@FirstName", SqlDbType.VarChar, employee.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, employee.LastName);
                AddParameter("@Email", SqlDbType.VarChar, user.Email);
                AddParameter("@EmployeeImage", SqlDbType.VarBinary, employee.EmployeeImage);
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
        public Employee GetAllEmployees(string Email)
        {
            Employee employee = new Employee();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            SqlCommand GetEmployees = new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = cnMaisonsConnection,
                CommandText = "GetAllEmployees"
            };

            SqlParameter EmailParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = Email
            };

            GetEmployees.Parameters.Add(EmailParameter);
            SqlDataReader MyDataReader = GetEmployees.ExecuteReader();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    employee.EmployeeId = (int)MyDataReader["EmployeeID"];               
                    employee.EmployeeImage = MyDataReader["EmployeeImage"] == DBNull.Value ? null! : (byte[])MyDataReader["EmployeeImage"];
                    employee.FirstName = (string)MyDataReader["FirstName"];
                    employee.LastName = (string)MyDataReader["LastName"];
                    employee.Email = (string)MyDataReader["Email"];
                    employee.DateJoined = (DateTime)MyDataReader["DateJoined"];
                }
            }
            MyDataReader.Close();
            cnMaisonsConnection.Close();
            return employee;
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            // Hash the password with PBKDF2 using HMACSHA256
            return new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256).GetBytes(32);
        }
    }
}
