using CNMaisons.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace CNMaisons.TechnicalService
{

    public class Visits
    {
        private string? connectionString;
        public Visits()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }



        public String CreatePropertyVisit(Visit propertyVisit)
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
                MyCommand.CommandText = "AddPropertyVisit";

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

                // Adding parameters
                AddParameter("@PropertyID", SqlDbType.VarChar, propertyVisit.PropertyID);
                AddParameter("@FirstName", SqlDbType.VarChar, propertyVisit.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, propertyVisit.LastName);
                AddParameter("@Email", SqlDbType.VarChar, propertyVisit.Email);
                AddParameter("@PhoneNumber", SqlDbType.VarChar, propertyVisit.PhoneNumber);
                AddParameter("@ProposedVisitDate", SqlDbType.Date, propertyVisit.ProposedVisitDate);
                AddParameter("@ProposedVisitTime", SqlDbType.Time, propertyVisit.ProposedVisitTime);
                AddParameter("@VisitStatus", SqlDbType.VarChar, propertyVisit.VisitStatus);


                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                Success = "Successful!";
            }
            
            catch (Exception ex)
            {
                Success = $"An error occurred: {ex.Message}";
            }
            return Success;

        }
    }
}
