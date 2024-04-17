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

        public List<Payment> GetTenantPaymentbyDate(string aTenantID, DateTime startDate, DateTime endDate)
        { 
              
            List<Payment> myPaymentList = new();

            try
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
                    CommandText = "ViewTenantPaymentByDateRange"
                };

                SqlParameter MyParameter;
                MyParameter = new()
                {
                    ParameterName = "@TenantID",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = aTenantID
                };
                MyCommand.Parameters.Add(MyParameter);

                MyParameter = new()
                {
                    ParameterName = "@StartDate",
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input,
                    SqlValue = startDate
                };
                MyCommand.Parameters.Add(MyParameter);

                MyParameter = new()
                {
                    ParameterName = "@EndDate",
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input,
                    SqlValue = endDate
                };
                MyCommand.Parameters.Add(MyParameter);

                Payment aPayment = new();

                SqlDataReader MyDataReader = MyCommand.ExecuteReader();
                int Count = 0;
                if (MyDataReader.HasRows)
                {
                    while (MyDataReader.Read())
                    {
                        aPayment = new()
                        {
                            PaymentID = (int)MyDataReader["PaymentID"]!,
                            TenantID = MyDataReader["TenantID"]?.ToString()!,
                            PropertyID = MyDataReader["PropertyID"]?.ToString()!,
                            AmountPaid = (decimal)MyDataReader["AmountPaid"],
                            PaymentStartMonth = MyDataReader["PaymentStartMonth"]?.ToString()!,
                            PaymentEndMonth = MyDataReader["PaymentEndMonth"]?.ToString()!,
                            PaymentStartYear = (int)MyDataReader["PaymentStartYear"],
                            MonthsPaidFor = (int)MyDataReader["MonthsPaidFor"],
                            NextDueMonth = MyDataReader["NextDueMonth"]?.ToString()!,
                            NextDueYear = (int)MyDataReader["NextDueYear"],
                            NextDueDate = MyDataReader["NextDueDate"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["NextDueDate"],
                            DateOfTenantsPayment =  MyDataReader["DateOfTenantsPayment"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfTenantsPayment"],
                            MethodOfPayment = MyDataReader["MethodOfPayment"]?.ToString()!,
                            TenantPaymentBank = MyDataReader["TenantPaymentBank"]?.ToString()!,
                            DateOfRecord =MyDataReader["DateOfRecord"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfRecord"]
                        };
                        myPaymentList.Add(aPayment);
                    }
                    Count++;
                    MyDataReader.Close();
                    MyDataSource.Close();
                }
            }

           
            catch (Exception ex)
            {
                // Handle other exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return myPaymentList;

        }

        public bool AddReminder(string tenantID, string description, DateTime dueDateRemindedFor)
        {
            bool success = false;
            string successMessage = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddReminders", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@TenantID", tenantID);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@DueDateRemindedFor", dueDateRemindedFor);

                        // Execute the command
                        command.ExecuteNonQuery();

                        success = true;
                        successMessage = "Reminder added successfully.";
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation error number
                {
                    successMessage = "This reminder already exists.";
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

            return success;
        }

        public List<Reminder> GetAllReminders()
        {
            List<Reminder> reminderList = new List<Reminder>();
            Reminder reminder = new Reminder();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();
            SqlCommand GetTenant = new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = cnMaisonsConnection,
                CommandText = "GetAllReminders"
            };

            SqlDataReader MyDataReader = GetTenant.ExecuteReader();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    reminder = new()
                    {
                        ReminderId = (int)MyDataReader["RemindersID"],
                        TenantID = (string)MyDataReader["TenantID"],
                        Description = (string)MyDataReader["Description"],
                        DateSent = (DateTime)MyDataReader["DateSent"],
                        DueDateRemindedFor = (DateTime)MyDataReader["DueDateRemindedFor"]
                    };
                    reminderList.Add(reminder);
                }
            }
            MyDataReader.Close();
            cnMaisonsConnection.Close();
            return reminderList;
        }
        public List<Payment> GetFinancialRecordbyDate(int findStartYear, int findEndYear)
        {

            List<Payment> myPaymentList = new();

            try
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
                    CommandText = "ViewFinancialRecordByDateRange"
                };

                SqlParameter MyParameter;
                MyParameter = new()
                {
                    ParameterName = "@StartDate",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = findStartYear
                };
                MyCommand.Parameters.Add(MyParameter);

                MyParameter = new()
                {
                    ParameterName = "@EndDate",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = findEndYear
                };
                MyCommand.Parameters.Add(MyParameter);

                Payment aPayment = new();

                SqlDataReader MyDataReader = MyCommand.ExecuteReader();
                int Count = 0;
                if (MyDataReader.HasRows)
                {
                    while (MyDataReader.Read())
                    {
                        aPayment = new()
                        {
                            PaymentID = (int)MyDataReader["PaymentID"],
                            TenantID = MyDataReader["TenantID"]?.ToString(),
                            PropertyID = MyDataReader["PropertyID"]?.ToString(),
                            AmountPaid = (decimal)MyDataReader["AmountPaid"],
                            PaymentStartMonth = MyDataReader["PaymentStartMonth"]?.ToString(),
                            PaymentEndMonth = MyDataReader["PaymentEndMonth"]?.ToString(),
                            PaymentStartYear = (int)MyDataReader["PaymentStartYear"],
                            MonthsPaidFor = (int)MyDataReader["MonthsPaidFor"],
                            NextDueMonth = MyDataReader["NextDueMonth"]?.ToString(),
                            NextDueYear = (int)MyDataReader["NextDueYear"],
                            NextDueDate = MyDataReader["NextDueDate"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["NextDueDate"],
                            DateOfTenantsPayment = MyDataReader["DateOfTenantsPayment"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfTenantsPayment"],
                            MethodOfPayment = MyDataReader["MethodOfPayment"]?.ToString(),
                            TenantPaymentBank = MyDataReader["TenantPaymentBank"]?.ToString(),
                            DateOfRecord = MyDataReader["DateOfRecord"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfRecord"]
                        };
                        myPaymentList.Add(aPayment);
                    }
                    Count++;
                    MyDataReader.Close();
                    MyDataSource.Close();
                }
            }


            catch (Exception ex)
            {
                // Handle other exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return myPaymentList;

        }


        public List<Payment> ViewFinancialRecordDueInSixMonths()
        {

            List<Payment> myPaymentList = new();

            try
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
                    CommandText = "ViewFinancialRecordDueInSixMonths"
                };

                Payment aPayment = new();

                SqlDataReader MyDataReader = MyCommand.ExecuteReader();
                int Count = 0;
                if (MyDataReader.HasRows)
                {
                    while (MyDataReader.Read())
                    {
                        aPayment = new()
                        {
                            PaymentID = (int)MyDataReader["PaymentID"],
                            TenantID = MyDataReader["TenantID"]?.ToString(),
                            PropertyID = MyDataReader["PropertyID"]?.ToString(),
                            AmountPaid = (decimal)MyDataReader["AmountPaid"],
                            PaymentStartMonth = MyDataReader["PaymentStartMonth"]?.ToString(),
                            PaymentEndMonth = MyDataReader["PaymentEndMonth"]?.ToString(),
                            PaymentStartYear = (int)MyDataReader["PaymentStartYear"],
                            MonthsPaidFor = (int)MyDataReader["MonthsPaidFor"],
                            NextDueMonth = MyDataReader["NextDueMonth"]?.ToString(),
                            NextDueYear = (int)MyDataReader["NextDueYear"],
                            NextDueDate = MyDataReader["NextDueDate"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["NextDueDate"],
                            DateOfTenantsPayment = MyDataReader["DateOfTenantsPayment"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfTenantsPayment"],
                            MethodOfPayment = MyDataReader["MethodOfPayment"]?.ToString(),
                            TenantPaymentBank = MyDataReader["TenantPaymentBank"]?.ToString(),
                            DateOfRecord = MyDataReader["DateOfRecord"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfRecord"]
                        };
                        myPaymentList.Add(aPayment);
                    }
                    Count++;
                    MyDataReader.Close();
                    MyDataSource.Close();
                }
            }


            catch (Exception ex)
            {
                // Handle other exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return myPaymentList;

        }

        public List<Payment> ViewFinancialRecordDueInThreeMonths()
        {

            List<Payment> myPaymentList = new();

            try
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
                    CommandText = "ViewFinancialRecordDueInThreeMonths"
                };

                Payment aPayment = new();

                SqlDataReader MyDataReader = MyCommand.ExecuteReader();
                int Count = 0;
                if (MyDataReader.HasRows)
                {
                    while (MyDataReader.Read())
                    {
                        aPayment = new()
                        {
                            PaymentID = (int)MyDataReader["PaymentID"],
                            TenantID = MyDataReader["TenantID"]?.ToString(),
                            PropertyID = MyDataReader["PropertyID"]?.ToString(),
                            AmountPaid = (decimal)MyDataReader["AmountPaid"],
                            PaymentStartMonth = MyDataReader["PaymentStartMonth"]?.ToString(),
                            PaymentEndMonth = MyDataReader["PaymentEndMonth"]?.ToString(),
                            PaymentStartYear = (int)MyDataReader["PaymentStartYear"],
                            MonthsPaidFor = (int)MyDataReader["MonthsPaidFor"],
                            NextDueMonth = MyDataReader["NextDueMonth"]?.ToString(),
                            NextDueYear = (int)MyDataReader["NextDueYear"],
                            NextDueDate = MyDataReader["NextDueDate"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["NextDueDate"],
                            DateOfTenantsPayment = MyDataReader["DateOfTenantsPayment"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfTenantsPayment"],
                            MethodOfPayment = MyDataReader["MethodOfPayment"]?.ToString(),
                            TenantPaymentBank = MyDataReader["TenantPaymentBank"]?.ToString(),
                            DateOfRecord = MyDataReader["DateOfRecord"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DateOfRecord"]
                        };
                        myPaymentList.Add(aPayment);
                    }
                    Count++;
                    MyDataReader.Close();
                    MyDataSource.Close();
                }
            }


            catch (Exception ex)
            {
                // Handle other exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return myPaymentList;

        }
    }
}
