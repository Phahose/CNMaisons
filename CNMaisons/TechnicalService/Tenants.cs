using Microsoft.Data.SqlClient;
using System.Data;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Rewrite;

namespace CNMaisons.TechnicalService
{
    public class Tenants
    {
        private string? connectionString;
        public Tenants()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }



        public string AddLeaseApplication(Tenant aTenant)
        {
            string Success;

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
                MyCommand.CommandText = "AddTenant";

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
                AddParameter("@TenantID", SqlDbType.VarChar, aTenant.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, aTenant.PropertyID);
                AddParameter("@Passport", SqlDbType.VarBinary, aTenant.Passport);
                AddParameter("@FirstName", SqlDbType.VarChar, aTenant.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, aTenant.LastName);
                AddParameter("@PhoneNumber", SqlDbType.VarChar, aTenant.PhoneNumber);
                AddParameter("@Email", SqlDbType.VarChar, aTenant.Email);
                AddParameter("@DOB", SqlDbType.DateTime, aTenant.DOB);
                AddParameter("@Password", SqlDbType.VarChar, aTenant.Password);
                AddParameter("@Nationality", SqlDbType.VarChar, aTenant.Nationality);

                AddParameter("@StateofOrigin", SqlDbType.VarChar, aTenant.StateofOrigin);
                AddParameter("@LGA", SqlDbType.VarChar, aTenant.LGA);
                AddParameter("@HomeTown", SqlDbType.VarChar, aTenant.HomeTown);
                AddParameter("@PermanentHomeAddress", SqlDbType.VarChar, aTenant.PermanentHomeAddress);
                AddParameter("@Occupation", SqlDbType.VarChar, aTenant.Occupation);
                AddParameter("@SelfEmployed", SqlDbType.VarChar, aTenant.SelfEmployed);
                AddParameter("@BusinessRegistrationNumber", SqlDbType.VarChar, aTenant.BusinessRegistrationNumber);
                AddParameter("@CoporateAffairsCertificate", SqlDbType.VarBinary, aTenant.CoporateAffairsCertificate);
                AddParameter("@NameofEmployer", SqlDbType.VarChar, aTenant.NameofEmployer);
                AddParameter("@AddressOfEmployer", SqlDbType.VarChar, aTenant.AddressOfEmployer);

                AddParameter("@LengthOnJob", SqlDbType.Int, aTenant.LengthOnJob);
                AddParameter("@CurrentPositionHeld", SqlDbType.VarChar, aTenant.CurrentPositionHeld);
                AddParameter("@NatureOfJob", SqlDbType.VarChar, aTenant.NatureOfJob);
                AddParameter("@FormerResidenceAddress", SqlDbType.VarChar, aTenant.FormerResidenceAddress);
                AddParameter("@ReasonForMoving", SqlDbType.VarChar, aTenant.ReasonForMoving);
                AddParameter("@LengthOfStayAtOldResidence", SqlDbType.Int, aTenant.LengthOfStayAtOldResidence);
                AddParameter("@NameOfFormerResidentManager", SqlDbType.VarChar, aTenant.NameOfFormerResidentManager);
                AddParameter("@ObjectionsToReasonsForMoving", SqlDbType.VarChar, aTenant.ObjectionsToReasonsForMoving);
                AddParameter("@MaritalStatus", SqlDbType.VarChar, aTenant.MaritalStatus);
                AddParameter("@SpouseFirstName", SqlDbType.VarChar, aTenant.SpouseFirstName);

                AddParameter("@SpouseLastName", SqlDbType.VarChar, aTenant.SpouseLastName);
                AddParameter("@SpouseOccupation", SqlDbType.VarChar, aTenant.SpouseOccupation);
                AddParameter("@NumberOfOccupants", SqlDbType.Int, aTenant.NumberOfOccupants);
                AddParameter("@NextOfKinFirstName", SqlDbType.VarChar, aTenant.NextOfKinFirstName);
                AddParameter("@NextOfKinLastName", SqlDbType.VarChar, aTenant.NextOfKinLastName);
                AddParameter("@NextOfKinAddress", SqlDbType.VarChar, aTenant.NextOfKinAddress);
                AddParameter("@NextOfKinPhoneNumber", SqlDbType.VarChar, aTenant.NextOfKinPhoneNumber);
                AddParameter("@Guarantor1FirstName", SqlDbType.VarChar, aTenant.Guarantor1FirstName);
                AddParameter("@Guarantor1LastName", SqlDbType.VarChar, aTenant.Guarantor1LastName);
                AddParameter("@Guarantor1Address", SqlDbType.VarChar, aTenant.Guarantor1Address);

                AddParameter("@Guarantor1Occupation", SqlDbType.VarChar, aTenant.Guarantor1Occupation);
                AddParameter("@Guarantor1PhoneNumber", SqlDbType.VarChar, aTenant.Guarantor1PhoneNumber);
                AddParameter("@Guarantor1AlternatePhoneNumber", SqlDbType.VarChar, aTenant.Guarantor1AlternatePhoneNumber);
                AddParameter("@Guarantor2FirstName", SqlDbType.VarChar, aTenant.Guarantor2FirstName);
                AddParameter("@Guarantor2LastName", SqlDbType.VarChar, aTenant.Guarantor2LastName);
                AddParameter("@Guarantor2Address", SqlDbType.VarChar, aTenant.Guarantor2Address);
                AddParameter("@Guarantor2Occupation", SqlDbType.VarChar, aTenant.Guarantor2Occupation);
                AddParameter("@Guarantor2PhoneNumber", SqlDbType.VarChar, aTenant.Guarantor2PhoneNumber);
                AddParameter("@Guarantor2AlternatePhoneNumber", SqlDbType.VarChar, aTenant.Guarantor2AlternatePhoneNumber);
                AddParameter("@Declaration", SqlDbType.VarChar, aTenant.Declaration);

                AddParameter("@YourSignature", SqlDbType.VarChar, aTenant.YourSignature);
                AddParameter("@ApprovalStatus", SqlDbType.VarChar, aTenant.ApprovalStatus);
                AddParameter("@DeleteFlag", SqlDbType.Bit, aTenant.DeleteFlag);

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

        public List<Tenant> RetrievePendingLeaseApplication()
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
                CommandText = "GetPendingLeaseApplication"
            };

            SqlDataReader MyDataReader = MyCommand.ExecuteReader();


            List<Tenant> myTenantPendingReviewList = new();
            Tenant aTenantPendingReview = new();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    aTenantPendingReview = new()
                    {
                        ApprovalStatus = (string)MyDataReader["ApprovalStatus"],
                        TenantID = (string)MyDataReader["TenantID"],
                        PropertyID = (string)MyDataReader["PropertyID"],
                        FirstName = (string)MyDataReader["FirstName"],
                        LastName = (string)MyDataReader["LastName"],
                    };
                    myTenantPendingReviewList.Add(aTenantPendingReview);
                }
            }
            MyCommand.ExecuteNonQuery();
            MyDataSource.Close();
            return myTenantPendingReviewList;
        }



        public Tenant GetTenantLeaseApplicationForReview(String TenantID)
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
                CommandText = "ViewTenant"
            };

            SqlDataReader MyDataReader = MyCommand.ExecuteReader();



            Tenant aTenantPendingReview = new();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    aTenantPendingReview = new()
                    {
                        ApprovalStatus = (string)MyDataReader["ApprovalStatus"],
                        TenantID = (string)MyDataReader["TenantID"],
                        PropertyID = (string)MyDataReader["PropertyID"],
                        FirstName = (string)MyDataReader["FirstName"],
                        LastName = (string)MyDataReader["LastName"],
                    };
                }
            }
            MyCommand.ExecuteNonQuery();
            MyDataSource.Close();

            return aTenantPendingReview;
        }
    }
}



