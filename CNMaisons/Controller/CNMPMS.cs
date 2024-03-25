using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using System.Security.Cryptography;

namespace CNMaisons.Controller
{
    public class CNMPMS
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

        public Employee GetUserByEmail(string existingUseremail)
        {
            Employee user = new Employee();
            UsersManager controll = new UsersManager();
            user = controll.GetUserByEmail(existingUseremail);
            return user;
        }

        public bool AddEmployee(Employee employee)
        {
            bool success = false;
            UsersManager users = new();
            byte[] salt = new byte[128/8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            byte[] hashedPassword = HashPasswordWithSalt(employee.Password, salt);

            // Convert the salt and hashed password to Base64 for storage
            string saltBase64 = Convert.ToBase64String(salt);
            string hashedPasswordBase64 = Convert.ToBase64String(hashedPassword);
            employee.Password = hashedPasswordBase64;
            employee.UserSalt = saltBase64;
            success = users.AddEmployee(employee);
            return success;
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            // Hash the password with PBKDF2 using HMACSHA256
            return new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256).GetBytes(32);
        }

        public string SubmitLeaseApplication(Tenant aTenant)
        {
            string Success;
            Tenants tenatManager = new();
            Success = tenatManager.AddLeaseApplication(aTenant);
            return Success;
        }
        public List<Tenant> GetPendingLeaseApplication()
        {
            List<Tenant> TenantsPendingApplicationReview=new();
            Tenants tenatManager = new();
            TenantsPendingApplicationReview = tenatManager.RetrievePendingLeaseApplication();
            return TenantsPendingApplicationReview;
        }
        
    }
}
