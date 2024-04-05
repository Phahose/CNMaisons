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
            SqlParameter PropertyPrice = new()
            {
                ParameterName = "PropertyPrice",
                SqlDbType = SqlDbType.Decimal,
                SqlValue = Property.PropertyPrice,
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
                AddPropertyCommand.Parameters.Add(PropertyPrice);
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

        public bool UpdateProperty(Property Property)
        {
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();
            bool confirmation = true;
            string message;
            SqlCommand UpdatePropetryCommand = new()
            {
                Connection = cnMaisonsConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "UpdateProperty",
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
            SqlParameter PropertyPrice = new()
            {
                ParameterName = "PropertyPrice",
                SqlDbType = SqlDbType.Decimal,
                SqlValue = Property.PropertyPrice,
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
                Direction = ParameterDirection.Input,
            };
            if (Property.Image1 != null)
            {
                Image1.Value = Property.Image1;
            }
            else
            {
                Image1.Value = DBNull.Value;
            }
            SqlParameter Image2 = new()
            {
                ParameterName = "@Image2",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image2 != null)
            {
                Image2.Value = Property.Image2;
            }
            else
            {
                Image2.Value = DBNull.Value;
            }
            SqlParameter Image3 = new()
            {
                ParameterName = "@Image3",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image3 != null)
            {
                Image3.Value = Property.Image3;
            }
            else
            {
                Image3.Value = DBNull.Value;
            }
            SqlParameter Image4 = new()
            {
                ParameterName = "@Image4",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image4 != null)
            {
                Image4.Value = Property.Image4;
            }
            else
            {
                Image4.Value = DBNull.Value;
            }
            SqlParameter Image5 = new()
            {
                ParameterName = "@Image5",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image5 != null)
            {
                Image5.Value = Property.Image5;
            }
            else
            {
                Image5.Value = DBNull.Value;
            }
            SqlParameter Image6 = new()
            {
                ParameterName = "@Image6",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image6 != null)
            {
                Image6.Value = Property.Image6;
            }
            else
            {
                Image4.Value = DBNull.Value;
            }
            SqlParameter Image7 = new()
            {
                ParameterName = "@Image7",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image7 != null)
            {
                Image7.Value = Property.Image7;
            }
            else
            {
                Image7.Value = DBNull.Value;
            }
            SqlParameter Image8 = new()
            {
                ParameterName = "@Image8",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image8 != null)
            {
                Image8.Value = Property.Image8;
            }
            else
            {
                Image8.Value = DBNull.Value;
            }
            SqlParameter Image9 = new()
            {
                ParameterName = "@Image9",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image9 != null)
            {
                Image9.Value = Property.Image9;
            }
            else
            {
                Image9.Value = DBNull.Value;
            }
            SqlParameter Image10 = new()
            {
                ParameterName = "@Image10",
                SqlDbType = SqlDbType.VarBinary,
                Direction = ParameterDirection.Input,
            };
            if (Property.Image10 != null)
            {
                Image10.Value = Property.Image10;
            }
            else
            {
                Image10.Value = DBNull.Value;
            }
            SqlParameter Occupied = new()
            {
                ParameterName = "@Occupied",
                SqlDbType = SqlDbType.Bit,
                SqlValue = Property.Occupied,
                Direction = ParameterDirection.Input,
            };

            try
            {
                UpdatePropetryCommand.Parameters.Add(PropertyIDParameter);
                UpdatePropetryCommand.Parameters.Add(PropertyName);
                UpdatePropetryCommand.Parameters.Add(PropertyLocationState);
                UpdatePropetryCommand.Parameters.Add(PropertyLocationCountry);
                UpdatePropetryCommand.Parameters.Add(PropertyAddress);
                UpdatePropetryCommand.Parameters.Add(PropertyType);
                UpdatePropetryCommand.Parameters.Add(NumberOfRooms);
                UpdatePropetryCommand.Parameters.Add(PropertyDescription);
                UpdatePropetryCommand.Parameters.Add(PropertyPrice);
                UpdatePropetryCommand.Parameters.Add(Image1);
                UpdatePropetryCommand.Parameters.Add(Image2);
                UpdatePropetryCommand.Parameters.Add(Image3);
                UpdatePropetryCommand.Parameters.Add(Image4);
                UpdatePropetryCommand.Parameters.Add(Image5);
                UpdatePropetryCommand.Parameters.Add(Image6);
                UpdatePropetryCommand.Parameters.Add(Image7);
                UpdatePropetryCommand.Parameters.Add(Image8);
                UpdatePropetryCommand.Parameters.Add(Image9);
                UpdatePropetryCommand.Parameters.Add(Image10);
                UpdatePropetryCommand.Parameters.Add(Occupied);

                UpdatePropetryCommand.ExecuteNonQuery();
                cnMaisonsConnection.Close();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                confirmation = false;
            }

            return confirmation;
        }

        public List<Property> GetProperty()
        {
            List<Property> properties = new List<Property>();
            SqlConnection cnMaisonsConnection = new SqlConnection();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();
            SqlCommand GetPropertyCommand = new()
            {
                Connection = cnMaisonsConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetProperty",
            };
            try
            {
                SqlDataReader propertyReader = GetPropertyCommand.ExecuteReader();
                if (propertyReader.HasRows)
                {
                    while (propertyReader.Read())
                    {
                        Property property = new Property
                        {
                            PropertyID = propertyReader["PropertyID"].ToString()!,
                            PropertyName = propertyReader["PropertyName"].ToString()!,
                            PropertyLocationState = propertyReader["PropertyLocationState"].ToString()!,
                            PropertyLocationCountry = propertyReader["PropertyLocationCountry"].ToString()!,
                            PropertyAddress = propertyReader["PropertyAddress"].ToString()!,
                            PropertyType = propertyReader["PropertyType"].ToString()!,
                            NumberOfRooms = Convert.ToInt32(propertyReader["NumberOfRooms"])!,
                            PropertyDescription = propertyReader["PropertyDescription"].ToString()!,
                            PropertyPrice = (decimal)propertyReader["PropertyPrice"],
                            Image1 = propertyReader["Image1"] == DBNull.Value ? null! : (byte[])propertyReader["Image1"],
                            Image2 = propertyReader["Image2"] == DBNull.Value ? null! : (byte[])propertyReader["Image2"],
                            Image3 = propertyReader["Image3"] == DBNull.Value ? null! : (byte[])propertyReader["Image3"],
                            Image4 = propertyReader["Image4"] == DBNull.Value ? null! : (byte[])propertyReader["Image4"],
                            Image5 = propertyReader["Image5"] == DBNull.Value ? null! : (byte[])propertyReader["Image5"],
                            Image6 = propertyReader["Image6"] == DBNull.Value ? null! : (byte[])propertyReader["Image6"],
                            Image7 = propertyReader["Image7"] == DBNull.Value ? null! : (byte[])propertyReader["Image7"],
                            Image8 = propertyReader["Image8"] == DBNull.Value ? null! : (byte[])propertyReader["Image8"],
                            Image9 = propertyReader["Image9"] == DBNull.Value ? null! : (byte[])propertyReader["Image9"],
                            Image10 = propertyReader["Image10"] == DBNull.Value ? null! : (byte[])propertyReader["Image10"],
                            Occupied = (bool)propertyReader["Occupied"]
                        };
                        properties.Add(property);
                    }
                }
               
                propertyReader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                cnMaisonsConnection.Close();
            }

            return properties;
        }

        public Property GetPropertyByID (string propertyId)
        {
            Property property = new Property();
            SqlConnection cnMaisonsConnection = new();
            cnMaisonsConnection.ConnectionString = connectionString;
            cnMaisonsConnection.Open();

            SqlCommand GetPropertyByIDCommand = new()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetPropertyByID",
                Connection = cnMaisonsConnection
            };

            SqlParameter PropertyIDParameter = new()
            {
                ParameterName = "PropertyID",
                SqlDbType = SqlDbType.VarChar,
                SqlValue = propertyId,
                Direction = ParameterDirection.Input,
            };

            GetPropertyByIDCommand.Parameters.Add(PropertyIDParameter);

            try
            {
                
                SqlDataReader propertyReader = GetPropertyByIDCommand.ExecuteReader();
                while (propertyReader.Read())
                {
                    property = new Property
                    {
                        PropertyID = propertyReader["PropertyID"].ToString()!,
                        PropertyName = propertyReader["PropertyName"].ToString()!,
                        PropertyLocationState = propertyReader["PropertyLocationState"].ToString()!,
                        PropertyLocationCountry = propertyReader["PropertyLocationCountry"].ToString()!,
                        PropertyAddress = propertyReader["PropertyAddress"].ToString()!,
                        PropertyType = propertyReader["PropertyType"].ToString()!,
                        NumberOfRooms = Convert.ToInt32(propertyReader["NumberOfRooms"])!,
                        PropertyDescription = propertyReader["PropertyDescription"].ToString()!,
                        PropertyPrice = (decimal)propertyReader["PropertyPrice"],
                        Image1 = propertyReader["Image1"] == DBNull.Value ? null! : (byte[])propertyReader["Image1"],
                        Image2 = propertyReader["Image2"] == DBNull.Value ? null! : (byte[])propertyReader["Image2"],
                        Image3 = propertyReader["Image3"] == DBNull.Value ? null! : (byte[])propertyReader["Image3"],
                        Image4 = propertyReader["Image4"] == DBNull.Value ? null! : (byte[])propertyReader["Image4"],
                        Image5 = propertyReader["Image5"] == DBNull.Value ? null! : (byte[])propertyReader["Image5"],
                        Image6 = propertyReader["Image6"] == DBNull.Value ? null! : (byte[])propertyReader["Image6"],
                        Image7 = propertyReader["Image7"] == DBNull.Value ? null! : (byte[])propertyReader["Image7"],
                        Image8 = propertyReader["Image8"] == DBNull.Value ? null! : (byte[])propertyReader["Image8"],
                        Image9 = propertyReader["Image9"] == DBNull.Value ? null! : (byte[])propertyReader["Image9"],
                        Image10 = propertyReader["Image10"] == DBNull.Value ? null! : (byte[])propertyReader["Image10"],
                        Occupied = (bool)propertyReader["Occupied"]
                    };
                }
                propertyReader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnMaisonsConnection.Close();
            }
            return property;
        }
        public bool DeleteProperty(string propertyID)
        {
            bool success = true;
            try
            {
                SqlConnection cnMaisonsConnection = new SqlConnection();
                cnMaisonsConnection.ConnectionString = connectionString;
                cnMaisonsConnection.Open();
                SqlCommand DeletePropertyCommand = new()
                {
                    Connection = cnMaisonsConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeleteProperty",
                };

                SqlParameter PropertyID = new SqlParameter()
                {
                    ParameterName = "@PropertyID",
                    SqlDbType = SqlDbType.VarChar,
                    SqlValue = propertyID,
                    Direction = ParameterDirection.Input,
                };

                DeletePropertyCommand.Parameters.Add(PropertyID);
                DeletePropertyCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
               success = false;
            }
            return success;
        }
    }
}
