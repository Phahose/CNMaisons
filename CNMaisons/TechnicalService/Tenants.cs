using Microsoft.Data.SqlClient;
using System.Data;
using CNMaisons.Domain;

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



        public bool  AddLeaseApplication(Tenant aTenant)
        {
            bool message=false;
            try
            {
                using (SqlConnection MyDataSource = new SqlConnection(connectionString))
                {
                    MyDataSource.Open();

                    using (SqlCommand MyCommand = new SqlCommand("AddTenant\r\n", MyDataSource))
                    {
                        MyCommand.CommandType = CommandType.StoredProcedure;



                        // Define a helper method to add parameters
                        void AddParameter(string parameterName, SqlDbType sqlDbType, object value)
                        {
                            MyCommand.Parameters.Add(new SqlParameter
                            {
                                ParameterName = parameterName,
                                SqlDbType = sqlDbType,
                                Direction = ParameterDirection.Input,
                                SqlValue = value
                            });
                        }

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
                        AddParameter("@Signature", SqlDbType.VarBinary, aTenant.Signature);
                        AddParameter("@ApprovalStatus", SqlDbType.VarChar, aTenant.ApprovalStatus);
                        AddParameter("@DeleteFlag", SqlDbType.Bit, aTenant.DeleteFlag);

                        // Execute the command
                        int rowsAffected = MyCommand.ExecuteNonQuery();

                        // If rows were affected, set Message to true
                        if (rowsAffected > 0)
                            message = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
                Console.WriteLine(ex.Message);
                // You might want to log the exception
            }

            return message;
        }


    }
}
