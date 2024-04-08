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


        public List<Visit> GetAllOpenPropertyVisit()
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
                CommandText = "GetAllOpenPropertyVisit"
            };

            SqlDataReader MyDataReader = MyCommand.ExecuteReader();

            List<Visit> myOpenVisitList = new();
            Visit myOpenVisit = new();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    myOpenVisit = new()
                    {
                        PropertyVisitID = (int)MyDataReader["PropertyVisitID"],
                        PropertyID = (string)MyDataReader["PropertyID"],
                        FirstName = (string)MyDataReader["FirstName"],
                        LastName = (string)MyDataReader["LastName"],
                        Email = (string)MyDataReader["Email"],
                        PhoneNumber = (string)MyDataReader["PhoneNumber"],
                        DateRecorded = (DateTime)MyDataReader["DateRecorded"],
                        ProposedVisitDate = (DateTime)MyDataReader["ProposedVisitDate"],
                        ProposedVisitTime = (TimeSpan)MyDataReader["ProposedVisitTime"],
                        VisitStatus = (string)MyDataReader["VisitStatus"]
                };
                    myOpenVisitList.Add(myOpenVisit);
                }
            }
            return myOpenVisitList;
        }


        
        public String ConfirmOrClosePropertyVisit(String findVisitID, string visitStatus)
        {
            String Success;

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
                MyCommand.CommandText = "ConfirmOrClosePropertyVisit";

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
                AddParameter("@PropertyVisitID", SqlDbType.VarChar, findVisitID);  
                AddParameter("@VisitStatus", SqlDbType.VarChar, visitStatus);


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

    }
}
