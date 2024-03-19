using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using System.Security.Cryptography;

namespace CNMaisons.Controller
{
    public class BCS
    {
        public bool AddProperty(Property property)
        {
            bool success = false;
            Properties properties = new Properties();
            success = properties.AddProperty(property);
            return success;
        }
         public bool UpdateProperty(Property property)
         {
            bool success = false;
            Properties properties = new Properties();
            success = properties.UpdateProperty(property);
            return success;
         }

        public bool DeleteProperty(string propertyID)
        {
            bool success = false;
            Properties properties = new Properties();
            success = properties.DeleteProperty(propertyID);
            return success;
        }
        public List<Property> GetProperties()
        {
            List<Property> PropertyList = new List<Property>();
            Properties Properties = new Properties();
            PropertyList = Properties.GetProperty();
            return PropertyList;
        }
         public Property GetPropertyByID(string propertyID)
         {
            Property Property = new();
            Properties Properties = new Properties();
            Property = Properties.GetPropertyByID(propertyID);
            return Property;
         }

        public User GetUserByEmail(string existingUseremail)
        {
            User user = new User();
            UsersManager controll = new UsersManager();
            user = controll.GetUserByEmail(existingUseremail);
            return user;
        }

        public bool AddUser(User user)
        {
            bool success = false;
            UsersManager users = new();
            byte[] salt = new byte[128/8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            byte[] hashedPassword = HashPasswordWithSalt(user.Password, salt);

            // Convert the salt and hashed password to Base64 for storage
            string saltBase64 = Convert.ToBase64String(salt);
            string hashedPasswordBase64 = Convert.ToBase64String(hashedPassword);
            user.Password = hashedPasswordBase64;
            user.UserSalt = saltBase64;
            success = users.AddUser(user);
            return success;
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            // Hash the password with PBKDF2 using HMACSHA256
            return new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256).GetBytes(32);
        }

        public bool SubmitLeaseApplication(Tenant aTenant)
        {
            bool success;
            Tenants tenatManager = new();
            success = tenatManager.AddLeaseApplication(aTenant);
            return success;
        }
    }
}
