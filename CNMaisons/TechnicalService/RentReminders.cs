using CNMaisons.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CNMaisons.TechnicalService
{
    public class RentReminders
    {

        private string? connectionString;
        public RentReminders()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }

        public bool AddRentReminder(RentReminder aReminder)
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
                MyCommand.CommandText = "AddRentReminder";

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


                AddParameter("@TenantID", SqlDbType.VarChar, aReminder.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, aReminder.PropertyID);
                AddParameter("@LastRentAmountPaid", SqlDbType.Money, aReminder.LastRentAmountPaid);
                AddParameter("@DateOfTenantsPayment", SqlDbType.Date, aReminder.DateOfTenantsPayment);
                AddParameter("@NextDueMonth", SqlDbType.VarChar, aReminder.NextDueMonth);
                AddParameter("@NextDueYear", SqlDbType.Int, aReminder.NextDueYear);
                AddParameter("@NextDueDate", SqlDbType.Date, aReminder.NextDueDate);


                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                successMessage = "Successful!";
                Success = true;
            }
           
            catch (Exception ex)
            {
                successMessage = $"An error occurred: {ex.Message}";
            }
            return Success;

        }

    }
}
