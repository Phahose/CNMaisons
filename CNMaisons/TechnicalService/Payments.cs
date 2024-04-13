using CNMaisons.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace CNMaisons.TechnicalService
{
    public class Payments
    {

        private string? connectionString;
        public Payments()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }

        public bool CreatePayment(Payment aPayment)
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
                MyCommand.CommandText = "AddPayment";

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


                AddParameter("@TenantID", SqlDbType.VarChar, aPayment.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, aPayment.PropertyID);
                AddParameter("@AmountPaid", SqlDbType.Money, aPayment.AmountPaid);
                AddParameter("@PaymentStartMonth", SqlDbType.VarChar, aPayment.PaymentStartMonth);
                AddParameter("@PaymentEndMonth", SqlDbType.VarChar, aPayment.PaymentEndMonth);
                AddParameter("@PaymentStartYear", SqlDbType.VarChar, aPayment.PaymentStartYear);
                AddParameter("@MonthsPaidFor", SqlDbType.Int, aPayment.MonthsPaidFor);
                AddParameter("@NextDueMonth", SqlDbType.VarChar, aPayment.NextDueMonth);
                AddParameter("@NextDueYear", SqlDbType.Int, aPayment.NextDueYear);
                AddParameter("@NextDueDate", SqlDbType.Date, aPayment.NextDueDate);
                AddParameter("@DateOfTenantsPayment", SqlDbType.Date, aPayment.DateOfTenantsPayment);
                AddParameter("@MethodOfPayment", SqlDbType.VarChar, aPayment.MethodOfPayment);
                AddParameter("@TenantPaymentBank", SqlDbType.VarChar, aPayment.TenantPaymentBank);

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
    }
}
