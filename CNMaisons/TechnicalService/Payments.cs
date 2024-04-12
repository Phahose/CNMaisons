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

        public bool AddPayment(Payment payment)
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


                AddParameter("@TenantID", SqlDbType.VarChar, payment.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, payment.PropertyID);
                AddParameter("@AmountPaid", SqlDbType.Money, payment.AmountPaid);
                AddParameter("@PaymentStartMonth", SqlDbType.VarChar, payment.PaymentStartMonth);
                AddParameter("@PaymentEndMonth", SqlDbType.VarChar, payment.PaymentEndMonth);
                AddParameter("@PaymentStartYear", SqlDbType.Int, payment.PaymentStartYear);
                AddParameter("@MonthsPaidFor", SqlDbType.Int, payment.MonthsPaidFor);
                AddParameter("@NextDueMonth", SqlDbType.Int, payment.NextDueMonth);
                AddParameter("@NextDueYear", SqlDbType.Int, payment.NextDueYear);
                AddParameter("@NextDueDate", SqlDbType.Date, payment.NextDueDate);
                AddParameter("@DateOfTenantsPayment", SqlDbType.Date, payment.DateOfTenantsPayment);
                AddParameter("@MethodOfPayment", SqlDbType.VarChar, payment.MethodOfPayment);
                AddParameter("@TenantPaymentBank", SqlDbType.Date, payment.TenantPaymentBank);



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
