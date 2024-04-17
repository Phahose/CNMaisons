#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Rewrite;
using System.Reflection.Metadata.Ecma335;
using System.Net.Mail;
using System.Net;

namespace CNMaisons.TechnicalService
{
    public class Tenants
    {
        private string connectionString;


        
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
                //AddParameter("@TenantID", SqlDbType.VarChar, aTenant.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, aTenant.PropertyID);
                AddParameter("@Passport", SqlDbType.VarBinary, aTenant.Passport);
                AddParameter("@FirstName", SqlDbType.VarChar, aTenant.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, aTenant.LastName);
                AddParameter("@PhoneNumber", SqlDbType.VarChar, aTenant.PhoneNumber);
                AddParameter("@Email", SqlDbType.VarChar, aTenant.Email);
                AddParameter("@DOB", SqlDbType.DateTime, aTenant.DOB);
                AddParameter("@Nationality", SqlDbType.VarChar, aTenant.Nationality);
                AddParameter("@StateofOrigin", SqlDbType.VarChar, aTenant.StateofOrigin);

                AddParameter("@LGA", SqlDbType.VarChar, aTenant.LGA);
                AddParameter("@HomeTown", SqlDbType.VarChar, aTenant.HomeTown);
                AddParameter("@PermanentHomeAddress", SqlDbType.VarChar, aTenant.PermanentHomeAddress);
                AddParameter("@Occupation", SqlDbType.VarChar, aTenant.Occupation);
                AddParameter("@SelfEmployed", SqlDbType.VarChar, aTenant.SelfEmployed);
                AddParameter("@BusinessRegistrationNumber", SqlDbType.VarChar, aTenant.BusinessRegistrationNumber);
                AddParameter("@CorporateAffairsCertificate", SqlDbType.VarBinary, aTenant.CorporateAffairsCertificate);
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
                AddParameter("@ApprovalStatus", SqlDbType.VarChar, aTenant.ApprovalStatus);
                AddParameter("@DeleteFlag", SqlDbType.Bit, aTenant.DeleteFlag);


                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                Success = "Successful.";
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
        
        
        
        public string ModifyLeaseApplication(Tenant aTenant)
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
                MyCommand.CommandText = "ModifyTenant";

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
                //AddParameter("@TenantID", SqlDbType.VarChar, aTenant.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, aTenant.PropertyID);
                AddParameter("@Passport", SqlDbType.VarBinary, aTenant.Passport);
                AddParameter("@FirstName", SqlDbType.VarChar, aTenant.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, aTenant.LastName);
                AddParameter("@PhoneNumber", SqlDbType.VarChar, aTenant.PhoneNumber);
                AddParameter("@Email", SqlDbType.VarChar, aTenant.Email);
                AddParameter("@DOB", SqlDbType.DateTime, aTenant.DOB);
                AddParameter("@Nationality", SqlDbType.VarChar, aTenant.Nationality);
                AddParameter("@StateofOrigin", SqlDbType.VarChar, aTenant.StateofOrigin);

                AddParameter("@LGA", SqlDbType.VarChar, aTenant.LGA);
                AddParameter("@HomeTown", SqlDbType.VarChar, aTenant.HomeTown);
                AddParameter("@PermanentHomeAddress", SqlDbType.VarChar, aTenant.PermanentHomeAddress);
                AddParameter("@Occupation", SqlDbType.VarChar, aTenant.Occupation);
                AddParameter("@SelfEmployed", SqlDbType.VarChar, aTenant.SelfEmployed);
                AddParameter("@BusinessRegistrationNumber", SqlDbType.VarChar, aTenant.BusinessRegistrationNumber);
                AddParameter("@CorporateAffairsCertificate", SqlDbType.VarBinary, aTenant.CorporateAffairsCertificate);
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
                AddParameter("@ApprovalStatus", SqlDbType.VarChar, aTenant.ApprovalStatus);
                AddParameter("@DeleteFlag", SqlDbType.Bit, aTenant.DeleteFlag);
                

                MyCommand.ExecuteNonQuery();
                MyDataSource.Close();
                Success = "Successful.";
            }

            catch (SqlException ex)
            {

                if (ex.Number == 2627) // Unique constraint violation error number
                {

                    Success = "This Tenant ID or the Email already exist. ";
                }
                else
                {
                    Success =  $"An error occurred: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                Success = $"An error occurred: {ex.Message}";
            }
            return Success;
        }






        public string ModifyTenant(Tenant aTenant)
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
                MyCommand.CommandText = "ModifyTenant";

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
                //AddParameter("@TenantID", SqlDbType.VarChar, aTenant.TenantID);
                AddParameter("@PropertyID", SqlDbType.VarChar, aTenant.PropertyID);
                AddParameter("@Passport", SqlDbType.VarBinary, aTenant.Passport);
                AddParameter("@FirstName", SqlDbType.VarChar, aTenant.FirstName);
                AddParameter("@LastName", SqlDbType.VarChar, aTenant.LastName);
                AddParameter("@PhoneNumber", SqlDbType.VarChar, aTenant.PhoneNumber);
                AddParameter("@Email", SqlDbType.VarChar, aTenant.Email);
                AddParameter("@DOB", SqlDbType.DateTime, aTenant.DOB);
                AddParameter("@Nationality", SqlDbType.VarChar, aTenant.Nationality);
                AddParameter("@StateofOrigin", SqlDbType.VarChar, aTenant.StateofOrigin);

                AddParameter("@LGA", SqlDbType.VarChar, aTenant.LGA);
                AddParameter("@HomeTown", SqlDbType.VarChar, aTenant.HomeTown);
                AddParameter("@PermanentHomeAddress", SqlDbType.VarChar, aTenant.PermanentHomeAddress);
                AddParameter("@Occupation", SqlDbType.VarChar, aTenant.Occupation);
                AddParameter("@SelfEmployed", SqlDbType.VarChar, aTenant.SelfEmployed);
                AddParameter("@BusinessRegistrationNumber", SqlDbType.VarChar, aTenant.BusinessRegistrationNumber);
                AddParameter("@CorporateAffairsCertificate", SqlDbType.VarBinary, aTenant.CorporateAffairsCertificate);
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
                AddParameter("@YourSignedForm", SqlDbType.VarChar, aTenant.YourSignedForm);

                AddParameter("@ApprovalStatus", SqlDbType.VarChar, aTenant.ApprovalStatus);
                AddParameter("@DeleteFlag", SqlDbType.Bit, aTenant.DeleteFlag);
                AddParameter("@LeaseFormForSigning", SqlDbType.VarBinary, aTenant.LeaseFormForSigning);

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
                        Email = (string)MyDataReader["Email"],
                        LeaseFormForSigning = MyDataReader["LeaseFormForSigning"] == DBNull.Value ? null! : (byte[])MyDataReader["LeaseFormForSigning"],
                        YourSignedForm = MyDataReader["YourSignedForm"] == DBNull.Value ? null! : (byte[])MyDataReader["YourSignedForm"]

                    };
                    myTenantPendingReviewList.Add(aTenantPendingReview);
                }
            }
            return myTenantPendingReviewList;
        }



        public Tenant RetrieveSpecificTenantApplication(String email)
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
                CommandText = "GetSpecificTenantApplication"
            };

            SqlParameter MyParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = email
            };
            MyCommand.Parameters.Add(MyParameter);

 
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
                        Email = (string)MyDataReader["Email"],
                        LeaseFormForSigning = MyDataReader["LeaseFormForSigning"] == DBNull.Value ? null! : (byte[])MyDataReader["LeaseFormForSigning"],
                        YourSignedForm = MyDataReader["YourSignedForm"] == DBNull.Value ? null! : (byte[])MyDataReader["YourSignedForm"]
                    };
                }
            }
            return aTenantPendingReview;
        }






        public Tenant GetTenantLeaseApplicationForReview(String myInput)
        {
            Tenant aTenantPendingReview = new();

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
                    CommandText = "ViewTenant"
                };

                SqlParameter MyParameter = new()
                {
                    ParameterName = "@myInput",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = myInput
                };
                MyCommand.Parameters.Add(MyParameter);


                SqlDataReader MyDataReader = MyCommand.ExecuteReader();
                int Count = 0;
                if (MyDataReader.HasRows)
                {
                    while (MyDataReader.Read())
                    {
                        aTenantPendingReview.Passport = MyDataReader["Passport"] == DBNull.Value ? null! : (byte[])MyDataReader["Passport"];
                        aTenantPendingReview.TenantID = MyDataReader["TenantID"]?.ToString();
                        aTenantPendingReview.PropertyID = MyDataReader["PropertyID"]?.ToString();
                        aTenantPendingReview.FirstName = MyDataReader["FirstName"]?.ToString();
                        aTenantPendingReview.LastName = MyDataReader["LastName"]?.ToString();
                        aTenantPendingReview.PhoneNumber = MyDataReader["PhoneNumber"]?.ToString();
                        aTenantPendingReview.Email = MyDataReader["Email"]?.ToString();
                        aTenantPendingReview.DOB = MyDataReader["DOB"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DOB"];
                        aTenantPendingReview.Nationality = MyDataReader["Nationality"]?.ToString();
                        aTenantPendingReview.StateofOrigin = MyDataReader["StateofOrigin"]?.ToString();
                        aTenantPendingReview.LGA = MyDataReader["LGA"]?.ToString();
                        aTenantPendingReview.HomeTown = MyDataReader["HomeTown"]?.ToString();
                        aTenantPendingReview.PermanentHomeAddress = MyDataReader["PermanentHomeAddress"]?.ToString();
                        aTenantPendingReview.Occupation = MyDataReader["Occupation"]?.ToString();
                        aTenantPendingReview.SelfEmployed = MyDataReader["SelfEmployed"]?.ToString();
                        aTenantPendingReview.BusinessRegistrationNumber = MyDataReader["BusinessRegistrationNumber"]?.ToString();
                        aTenantPendingReview.CorporateAffairsCertificate = MyDataReader["CorporateAffairsCertificate"] == DBNull.Value ? null! : (byte[])MyDataReader["CorporateAffairsCertificate"];
                        aTenantPendingReview.NameofEmployer = MyDataReader["NameofEmployer"]?.ToString();
                        aTenantPendingReview.AddressOfEmployer = MyDataReader["AddressOfEmployer"]?.ToString();
                        aTenantPendingReview.LengthOnJob = (int)MyDataReader["LengthOnJob"];
                        aTenantPendingReview.CurrentPositionHeld = MyDataReader["CurrentPositionHeld"]?.ToString();
                        aTenantPendingReview.NatureOfJob = MyDataReader["NatureOfJob"]?.ToString();
                        aTenantPendingReview.FormerResidenceAddress = MyDataReader["FormerResidenceAddress"]?.ToString();
                        aTenantPendingReview.ReasonForMoving = MyDataReader["ReasonForMoving"]?.ToString();
                        aTenantPendingReview.LengthOfStayAtOldResidence = (int)MyDataReader["LengthOfStayAtOldResidence"];
                        aTenantPendingReview.NameOfFormerResidentManager = MyDataReader["NameOfFormerResidentManager"]?.ToString();
                        aTenantPendingReview.ObjectionsToReasonsForMoving = MyDataReader["ObjectionsToReasonsForMoving"]?.ToString();
                        aTenantPendingReview.MaritalStatus = MyDataReader["MaritalStatus"]?.ToString();
                        aTenantPendingReview.SpouseFirstName = MyDataReader["SpouseFirstName"]?.ToString();
                        aTenantPendingReview.SpouseLastName = MyDataReader["SpouseLastName"]?.ToString();
                        aTenantPendingReview.SpouseOccupation = MyDataReader["SpouseOccupation"]?.ToString();
                        aTenantPendingReview.NumberOfOccupants = (int)MyDataReader["NumberOfOccupants"];
                        aTenantPendingReview.NextOfKinFirstName = MyDataReader["NextOfKinFirstName"]?.ToString();
                        aTenantPendingReview.NextOfKinLastName = MyDataReader["NextOfKinLastName"]?.ToString();
                        aTenantPendingReview.NextOfKinAddress = MyDataReader["NextOfKinAddress"]?.ToString();
                        aTenantPendingReview.NextOfKinPhoneNumber = MyDataReader["NextOfKinPhoneNumber"]?.ToString();
                        aTenantPendingReview.Guarantor1FirstName = MyDataReader["Guarantor1FirstName"]?.ToString();
                        aTenantPendingReview.Guarantor1LastName = MyDataReader["Guarantor1LastName"]?.ToString();
                        aTenantPendingReview.Guarantor1Address = MyDataReader["Guarantor1Address"]?.ToString();
                        aTenantPendingReview.Guarantor1Occupation = MyDataReader["Guarantor1Occupation"]?.ToString();
                        aTenantPendingReview.Guarantor1PhoneNumber = MyDataReader["Guarantor1PhoneNumber"]?.ToString();
                        aTenantPendingReview.Guarantor1AlternatePhoneNumber = MyDataReader["Guarantor1AlternatePhoneNumber"]?.ToString();
                        aTenantPendingReview.Guarantor2FirstName = MyDataReader["Guarantor2FirstName"]?.ToString();
                        aTenantPendingReview.Guarantor2LastName = MyDataReader["Guarantor2LastName"]?.ToString();
                        aTenantPendingReview.Guarantor2Address = MyDataReader["Guarantor2Address"]?.ToString();
                        aTenantPendingReview.Guarantor2Occupation = MyDataReader["Guarantor2Occupation"]?.ToString();
                        aTenantPendingReview.Guarantor2PhoneNumber = MyDataReader["Guarantor2PhoneNumber"]?.ToString();
                        aTenantPendingReview.Guarantor2AlternatePhoneNumber = MyDataReader["Guarantor2AlternatePhoneNumber"]?.ToString();
                        aTenantPendingReview.Declaration = MyDataReader["Declaration"]?.ToString();
                        aTenantPendingReview.YourSignedForm = MyDataReader["YourSignedForm"] == DBNull.Value ? null! : (byte[])MyDataReader["YourSignedForm"];
                        aTenantPendingReview.DeleteFlag = (bool)MyDataReader["DeleteFlag"];
                        aTenantPendingReview.ApprovalStatus = MyDataReader["ApprovalStatus"]?.ToString();
                        aTenantPendingReview.LeaseFormForSigning = MyDataReader["LeaseFormForSigning"] == DBNull.Value ? null! : (byte[])MyDataReader["LeaseFormForSigning"];

                    }
                    Count++;
                    MyDataReader.Close();
                    MyDataSource.Close();
                }
            }
                
            catch (SqlException ex)
            {
                // Handle SQL Server exceptions here
                Console.WriteLine($"An SQL error occurred. Error number: {ex.Number}, Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return aTenantPendingReview;
        }






        public String UpdateApplication(String findTenantID, String approvalStatus, byte[] LeaseForm)
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
                MyCommand.CommandText = "UpdateApplication";

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
                AddParameter("@TenantID", SqlDbType.VarChar, findTenantID);
                AddParameter("@ApprovalStatus", SqlDbType.VarChar, approvalStatus);
                AddParameter("@LeaseFormForSigning", SqlDbType.VarBinary, LeaseForm);


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



        public String ApproveOrRejectApplication(String findTenantID, String approvalStatus)
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
                MyCommand.CommandText = "UpdateFinalApplicationStatus";

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
                AddParameter("@TenantID", SqlDbType.VarChar, findTenantID);
                AddParameter("@ApprovalStatus", SqlDbType.VarChar, approvalStatus); 


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




        public String UpdateSignedApplication(String findTenantID, String approvalStatus, byte[] signedForm)
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
                MyCommand.CommandText = "UpdateSignedApplication";

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
                AddParameter("@TenantID", SqlDbType.VarChar, findTenantID);
                AddParameter("@ApprovalStatus", SqlDbType.VarChar, approvalStatus);
                AddParameter("@YourSignedForm", SqlDbType.VarBinary, signedForm);


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





        public Tenant GetTenant (string Email)
        {
            Tenant tenant = new Tenant();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();
            SqlCommand GetTenant = new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = cnMaisonsConnection,
                CommandText = "GetAllTenants"
            };

            SqlParameter EmailParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = Email
            };


            GetTenant.Parameters.Add(EmailParameter);
            SqlDataReader MyDataReader = GetTenant.ExecuteReader();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    tenant = new()
                    {
                        Passport = MyDataReader["Passport"] == DBNull.Value ? null! : (byte[])MyDataReader["Passport"],
                        TenantID = MyDataReader["TenantID"]?.ToString(),
                        PropertyID = MyDataReader["PropertyID"]?.ToString(),
                        FirstName = MyDataReader["FirstName"]?.ToString(),
                        LastName = MyDataReader["LastName"]?.ToString(),
                        PhoneNumber = MyDataReader["PhoneNumber"]?.ToString(),
                        Email = MyDataReader["Email"]?.ToString(),
                        DOB = MyDataReader["DOB"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DOB"],
                        Nationality = MyDataReader["Nationality"]?.ToString(),
                        StateofOrigin = MyDataReader["StateofOrigin"]?.ToString(),
                        LGA = MyDataReader["LGA"]?.ToString(),
                        HomeTown = MyDataReader["HomeTown"]?.ToString(),
                        PermanentHomeAddress = MyDataReader["PermanentHomeAddress"]?.ToString(),
                        Occupation = MyDataReader["Occupation"]?.ToString(),
                        SelfEmployed = MyDataReader["SelfEmployed"]?.ToString(),
                        BusinessRegistrationNumber = MyDataReader["BusinessRegistrationNumber"]?.ToString(),
                        CorporateAffairsCertificate = MyDataReader["CorporateAffairsCertificate"] == DBNull.Value ? null! : (byte[])MyDataReader["CorporateAffairsCertificate"],
                        NameofEmployer = MyDataReader["NameofEmployer"]?.ToString(),
                        AddressOfEmployer = MyDataReader["AddressOfEmployer"]?.ToString(),
                        LengthOnJob = (int)MyDataReader["LengthOnJob"],
                        CurrentPositionHeld = MyDataReader["CurrentPositionHeld"]?.ToString(),
                        NatureOfJob = MyDataReader["NatureOfJob"]?.ToString(),
                        FormerResidenceAddress = MyDataReader["FormerResidenceAddress"]?.ToString(),
                        ReasonForMoving = MyDataReader["ReasonForMoving"]?.ToString(),
                        LengthOfStayAtOldResidence = (int)MyDataReader["LengthOfStayAtOldResidence"],
                        NameOfFormerResidentManager = MyDataReader["NameOfFormerResidentManager"]?.ToString(),
                        ObjectionsToReasonsForMoving = MyDataReader["ObjectionsToReasonsForMoving"]?.ToString(),
                        MaritalStatus = MyDataReader["MaritalStatus"]?.ToString(),
                        SpouseFirstName = MyDataReader["SpouseFirstName"]?.ToString(),
                        SpouseLastName = MyDataReader["SpouseLastName"]?.ToString(),
                        SpouseOccupation = MyDataReader["SpouseOccupation"]?.ToString(),
                        NumberOfOccupants = (int)MyDataReader["NumberOfOccupants"],
                        NextOfKinFirstName = MyDataReader["NextOfKinFirstName"]?.ToString(),
                        NextOfKinLastName = MyDataReader["NextOfKinLastName"]?.ToString(),
                        NextOfKinAddress = MyDataReader["NextOfKinAddress"]?.ToString(),
                        NextOfKinPhoneNumber = MyDataReader["NextOfKinPhoneNumber"]?.ToString(),
                        Guarantor1FirstName = MyDataReader["Guarantor1FirstName"]?.ToString(),
                        Guarantor1LastName = MyDataReader["Guarantor1LastName"]?.ToString(),
                        Guarantor1Address = MyDataReader["Guarantor1Address"]?.ToString(),
                        Guarantor1Occupation = MyDataReader["Guarantor1Occupation"]?.ToString(),
                        Guarantor1PhoneNumber = MyDataReader["Guarantor1PhoneNumber"]?.ToString(),
                        Guarantor1AlternatePhoneNumber = MyDataReader["Guarantor1AlternatePhoneNumber"]?.ToString(),
                        Guarantor2FirstName = MyDataReader["Guarantor2FirstName"]?.ToString(),
                        Guarantor2LastName = MyDataReader["Guarantor2LastName"]?.ToString(),
                        Guarantor2Address = MyDataReader["Guarantor2Address"]?.ToString(),
                        Guarantor2Occupation = MyDataReader["Guarantor2Occupation"]?.ToString(),
                        Guarantor2PhoneNumber = MyDataReader["Guarantor2PhoneNumber"]?.ToString(),
                        Guarantor2AlternatePhoneNumber = MyDataReader["Guarantor2AlternatePhoneNumber"]?.ToString(),
                        Declaration = MyDataReader["Declaration"]?.ToString(),
                        YourSignedForm = MyDataReader["YourSignedForm"] == DBNull.Value ? null! : (byte[])MyDataReader["YourSignedForm"],
                        DeleteFlag = (bool)MyDataReader["DeleteFlag"],
                        ApprovalStatus = MyDataReader["ApprovalStatus"]?.ToString(),
                        LeaseFormForSigning = MyDataReader["LeaseFormForSigning"] == DBNull.Value ? null! : (byte[])MyDataReader["LeaseFormForSigning"],
                        NextRentDue = (DateTime)MyDataReader["NextRentDue"]
                    };
                }
            }
            MyDataReader.Close();
            cnMaisonsConnection.Close();
            return tenant;
        }

        public List<Tenant> GetAllTenants()
        {
            List<Tenant> tenantList = new List<Tenant>();
            Tenant tenant = new Tenant();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();
            SqlCommand GetTenant = new()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = cnMaisonsConnection,
                CommandText = "GetTenants"
            };

            SqlDataReader MyDataReader = GetTenant.ExecuteReader();

            if (MyDataReader.HasRows)
            {
                while (MyDataReader.Read())
                {
                    tenant = new()
                    {
                        Passport = MyDataReader["Passport"] == DBNull.Value ? null! : (byte[])MyDataReader["Passport"],
                        TenantID = MyDataReader["TenantID"]?.ToString(),
                        PropertyID = MyDataReader["PropertyID"]?.ToString(),
                        FirstName = MyDataReader["FirstName"]?.ToString(),
                        LastName = MyDataReader["LastName"]?.ToString(),
                        PhoneNumber = MyDataReader["PhoneNumber"]?.ToString(),
                        Email = MyDataReader["Email"]?.ToString(),
                        DOB = MyDataReader["DOB"] == DBNull.Value ? default(DateTime) : (DateTime)MyDataReader["DOB"],
                        Nationality = MyDataReader["Nationality"]?.ToString(),
                        StateofOrigin = MyDataReader["StateofOrigin"]?.ToString(),
                        LGA = MyDataReader["LGA"]?.ToString(),
                        HomeTown = MyDataReader["HomeTown"]?.ToString(),
                        PermanentHomeAddress = MyDataReader["PermanentHomeAddress"]?.ToString(),
                        Occupation = MyDataReader["Occupation"]?.ToString(),
                        SelfEmployed = MyDataReader["SelfEmployed"]?.ToString(),
                        BusinessRegistrationNumber = MyDataReader["BusinessRegistrationNumber"]?.ToString(),
                        CorporateAffairsCertificate = MyDataReader["CorporateAffairsCertificate"] == DBNull.Value ? null! : (byte[])MyDataReader["CorporateAffairsCertificate"],
                        NameofEmployer = MyDataReader["NameofEmployer"]?.ToString(),
                        AddressOfEmployer = MyDataReader["AddressOfEmployer"]?.ToString(),
                        LengthOnJob = (int)MyDataReader["LengthOnJob"],
                        CurrentPositionHeld = MyDataReader["CurrentPositionHeld"]?.ToString(),
                        NatureOfJob = MyDataReader["NatureOfJob"]?.ToString(),
                        FormerResidenceAddress = MyDataReader["FormerResidenceAddress"]?.ToString(),
                        ReasonForMoving = MyDataReader["ReasonForMoving"]?.ToString(),
                        LengthOfStayAtOldResidence = (int)MyDataReader["LengthOfStayAtOldResidence"],
                        NameOfFormerResidentManager = MyDataReader["NameOfFormerResidentManager"]?.ToString(),
                        ObjectionsToReasonsForMoving = MyDataReader["ObjectionsToReasonsForMoving"]?.ToString(),
                        MaritalStatus = MyDataReader["MaritalStatus"]?.ToString(),
                        SpouseFirstName = MyDataReader["SpouseFirstName"]?.ToString(),
                        SpouseLastName = MyDataReader["SpouseLastName"]?.ToString(),
                        SpouseOccupation = MyDataReader["SpouseOccupation"]?.ToString(),
                        NumberOfOccupants = (int)MyDataReader["NumberOfOccupants"],
                        NextOfKinFirstName = MyDataReader["NextOfKinFirstName"]?.ToString(),
                        NextOfKinLastName = MyDataReader["NextOfKinLastName"]?.ToString(),
                        NextOfKinAddress = MyDataReader["NextOfKinAddress"]?.ToString(),
                        NextOfKinPhoneNumber = MyDataReader["NextOfKinPhoneNumber"]?.ToString(),
                        Guarantor1FirstName = MyDataReader["Guarantor1FirstName"]?.ToString(),
                        Guarantor1LastName = MyDataReader["Guarantor1LastName"]?.ToString(),
                        Guarantor1Address = MyDataReader["Guarantor1Address"]?.ToString(),
                        Guarantor1Occupation = MyDataReader["Guarantor1Occupation"]?.ToString(),
                        Guarantor1PhoneNumber = MyDataReader["Guarantor1PhoneNumber"]?.ToString(),
                        Guarantor1AlternatePhoneNumber = MyDataReader["Guarantor1AlternatePhoneNumber"]?.ToString(),
                        Guarantor2FirstName = MyDataReader["Guarantor2FirstName"]?.ToString(),
                        Guarantor2LastName = MyDataReader["Guarantor2LastName"]?.ToString(),
                        Guarantor2Address = MyDataReader["Guarantor2Address"]?.ToString(),
                        Guarantor2Occupation = MyDataReader["Guarantor2Occupation"]?.ToString(),
                        Guarantor2PhoneNumber = MyDataReader["Guarantor2PhoneNumber"]?.ToString(),
                        Guarantor2AlternatePhoneNumber = MyDataReader["Guarantor2AlternatePhoneNumber"]?.ToString(),
                        Declaration = MyDataReader["Declaration"]?.ToString(),
                        YourSignedForm = MyDataReader["YourSignedForm"] == DBNull.Value ? null! : (byte[])MyDataReader["YourSignedForm"],
                        DeleteFlag = (bool)MyDataReader["DeleteFlag"],
                        ApprovalStatus = MyDataReader["ApprovalStatus"]?.ToString(),
                        LeaseFormForSigning = MyDataReader["LeaseFormForSigning"] == DBNull.Value ? null! : (byte[])MyDataReader["LeaseFormForSigning"],
                        NextRentDue = (DateTime)MyDataReader["NextRentDue"]
                    };
                    tenantList.Add(tenant);
                }
            }
            MyDataReader.Close();
            cnMaisonsConnection.Close();
            return tenantList;
        }

        public bool CheckIfEmailAlreadyExistInTenantTable(string email)
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
                CommandText = "CheckIfEmailAlreadyExistInTenantTable"
            };

            SqlParameter MyParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = email,
                Direction = ParameterDirection.Input,
            };
            MyCommand.Parameters.Add(MyParameter);
            
            SqlDataReader MyDataReader = MyCommand.ExecuteReader();
            bool Success = false;

            if (MyDataReader.HasRows)
            {
                Success = true;
            }
            return Success;
        }




        

    }
}