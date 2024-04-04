using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using System.Net.Mail;
using System.Net;
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

        public User GetUserByEmail(string existingUseremail)
        {
            User user = new User();
            Users RequestManager = new Users();
            user = RequestManager.GetUserByEmail(existingUseremail);
            return user;
        }

        public bool CreateUserAccount(User aUserAccount)
        {

            bool Success;
            Users RequestManager = new();
            Success = RequestManager.AddUser(aUserAccount);
            return Success;
        }

        public bool CreateEmployeeAccount(User aUserAccount, Employee anEmplyeeAccount)
        {

            bool Success;
            Employees RequestManager = new();
            Success = RequestManager.AddEmployee(aUserAccount, anEmplyeeAccount);
            return Success;
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
        public Tenant ViewTenant(String aTenantID)
        {
            Tenant aTenants = new();
            Tenants RequestManager = new();
            aTenants = RequestManager.GetTenantLeaseApplicationForReview(aTenantID);
            return aTenants;
        }
        public String ReviewApplication(String findTenantID, String approvalStatus)
        {
            String Success;
            Tenants RequestManager = new();
            Success = RequestManager.UpdateApplication(findTenantID, approvalStatus);
            return Success;
        }
        public Employee GetAllEmployees(string Email)
        {
            Employee employee = new Employee();
            Employees employees = new Employees();
            employee = employees.GetAllEmployees(Email);
            return employee;
        }
        public Tenant GetAllTennants (string email)
        {
            Tenant tenant = new();
            Tenants Tennants = new();
            tenant = Tennants.GetTenant(email);

            return tenant;
        }

        public bool ModifyUser (User user)
        {
            bool result = false;
            Users users = new Users();
            result = users.UpdateUser(user);
            return result;
        }
        static void SendReminderEmail(string TO_EMAIL_ADDRESS, string FROM_EMAIL_ADDRESS, string messageBody, string messageSubject)
        {
            try
            {


                // Create the email client object
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(FROM_EMAIL_ADDRESS, "ublu yfxa glrj esne ");

                    // Create the email message
                    MailMessage message = new MailMessage(FROM_EMAIL_ADDRESS, TO_EMAIL_ADDRESS);
                    message.Subject = messageSubject;
                    message.Body = messageBody;


                    // Send the email
                    client.Send(message);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

        }
    }
}
