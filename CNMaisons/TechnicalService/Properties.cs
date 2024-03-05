using Microsoft.Data.SqlClient;
using System.Data;
using CNMaisons.Domain;
namespace CNMaisons.TechnicalService
{
    public class Properties
    {
        private string? connectionString;
        public Properties()
        {
            ConfigurationBuilder DatabaseUserBuilder = new ConfigurationBuilder();
            DatabaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUserConfiguration = DatabaseUserBuilder.Build();
            connectionString = DatabaseUserConfiguration.GetConnectionString("CNMaisons");
        }


        public bool AddProperty(Property Property)
        {
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();
            bool confirmation = true;
            string message;
            SqlCommand AddPropertyCommand = new()
            {
                Connection = cnMaisonsConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddProperty",
            };
            SqlParameter PropertyIDParameter = new()
            {
                ParameterName = "@PropertyID",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyID,
                Direction = ParameterDirection.Input,
            };
            SqlParameter PropertyName = new()
            {
                ParameterName = "@PropertyName",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyName,
                Direction = ParameterDirection.Input,
            };
            SqlParameter PropertyLocationState = new()
            {
                ParameterName = "@PropertyLocationState",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyLocationState,
                 Direction = ParameterDirection.Input,
            };
            SqlParameter PropertyLocationCountry = new()
            {
                ParameterName = "@PropertyLocationCountry",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyLocationCountry,
                Direction = ParameterDirection.Input,
            };
            SqlParameter @PropertyAddress = new()
            {
                ParameterName = "@PropertyAddress",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyAddress
            };
            SqlParameter PropertyType = new()
            {
                ParameterName = "@PropertyType",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyType,
                Direction = ParameterDirection.Input,
            };
            SqlParameter NumberOfRooms = new()
            {
                ParameterName = "NumberOfRooms",
                SqlDbType = SqlDbType.Int,
                SqlValue = Property.NumberOfRooms,
                Direction = ParameterDirection.Input,
            };
            SqlParameter PropertyDescription = new()
            {
                ParameterName = "@PropertyDescription",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = Property.PropertyType,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image1 = new()
            {
                ParameterName = "@Image1",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image1,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image2 = new()
            {
                ParameterName = "@Image2",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image2,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image3 = new()
            {
                ParameterName = "@Image3",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image3,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image4 = new()
            {
                ParameterName = "@Image4",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image4,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image5 = new()
            {
                ParameterName = "@Image5",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image5,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image6 = new()
            {
                ParameterName = "@Image6",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image6,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image7 = new()
            {
                ParameterName = "@Image7",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image7,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image8 = new()
            {
                ParameterName = "@Image8",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image8,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image9 = new()
            {
                ParameterName = "@Image9",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image9,
                Direction = ParameterDirection.Input,
            };
            SqlParameter Image10 = new()
            {
                ParameterName = "@Image10",
                SqlDbType = SqlDbType.VarBinary,
                SqlValue = Property.Image10,
                Direction = ParameterDirection.Input,
            };

            try
            {
                AddPropertyCommand.Parameters.Add(PropertyIDParameter);
                AddPropertyCommand.Parameters.Add(PropertyName);
                AddPropertyCommand.Parameters.Add(PropertyLocationState);
                AddPropertyCommand.Parameters.Add(PropertyLocationCountry);
                AddPropertyCommand.Parameters.Add(PropertyAddress);
                AddPropertyCommand.Parameters.Add(PropertyType);
                AddPropertyCommand.Parameters.Add(NumberOfRooms);
                AddPropertyCommand.Parameters.Add(PropertyDescription);
                AddPropertyCommand.Parameters.Add(Image1);
                AddPropertyCommand.Parameters.Add(Image2);
                AddPropertyCommand.Parameters.Add(Image3);
                AddPropertyCommand.Parameters.Add(Image4);
                AddPropertyCommand.Parameters.Add(Image5);
                AddPropertyCommand.Parameters.Add(Image6);
                AddPropertyCommand.Parameters.Add(Image7);
                AddPropertyCommand.Parameters.Add(Image8);
                AddPropertyCommand.Parameters.Add(Image9);
                AddPropertyCommand.Parameters.Add(Image10);

                AddPropertyCommand.ExecuteNonQuery();
                cnMaisonsConnection.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                confirmation = false;  
            }
            
            return confirmation;
        }
    }
}
