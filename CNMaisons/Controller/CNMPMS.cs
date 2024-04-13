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

        public string UpdateLeaseApplication(Tenant aTenant)
        {
            string Success;
            Tenants tenatManager = new();
            Success = tenatManager.ModifyLeaseApplication(aTenant);
            return Success;
        }

        public List<Tenant> GetPendingLeaseApplication()
        {
            List<Tenant> TenantsPendingApplicationReview=new();
            Tenants tenatManager = new();
            TenantsPendingApplicationReview = tenatManager.RetrievePendingLeaseApplication();
            return TenantsPendingApplicationReview;
        }
         

        public Tenant GetSpecificTenantApplication(string email)
        {
            Tenant aTenantForReview = new();
            Tenants tenatRequestManager = new();
            aTenantForReview = tenatRequestManager.RetrieveSpecificTenantApplication(email);
            return aTenantForReview;
        }
        public Tenant ViewTenant(String email)
        {
            Tenant aTenants = new();
            Tenants RequestManager = new();
            aTenants = RequestManager.GetTenantLeaseApplicationForReview(email);
            return aTenants;
        }
        public String ReviewAwaitingApplication(String findTenantID, String approvalStatus, byte[] LeaseForm)
        {
            String Success;
            Tenants RequestManager = new();
            Success = RequestManager.UpdateApplication(findTenantID, approvalStatus, LeaseForm);
            return Success;
        }

        public String ApproveOrRejectLeaseApplication(string findTenantID, string approvalStatus)
        {
            String Success;
            Tenants RequestManager = new();
            Success = RequestManager.ApproveOrRejectApplication(findTenantID, approvalStatus);
            return Success;
        }

        public bool AddPayment(Payment aPayment)
        {
            bool Success;
            Payments PaymentRequestManager = new Payments();
            Success = PaymentRequestManager.CreatePayment(aPayment);
            return Success;
        }

        public String SubmitSignedCopy(String findTenantID, String approvalStatus, byte[] signedForm)
        {
            String Success;
            Tenants RequestManager = new();
            Success = RequestManager.UpdateSignedApplication(findTenantID, approvalStatus, signedForm);
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


        public static void SendReminderEmail(string TO_EMAIL_ADDRESS, string FROM_EMAIL_ADDRESS, string messageBody, string messageSubject)
        {
            try
            {
                // Create the email client object
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(FROM_EMAIL_ADDRESS, "ublu yfxa glrj esne "); //NIcholas
                     

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



        public bool ValidateEmailTenant(string email)
        {
            bool success = false;
            Tenants TenantRequestManager= new Tenants();
            success = TenantRequestManager.CheckIfEmailAlreadyExistInTenantTable(email);
            return success;
        }


        public bool ValidateEmailUsers(string email)
        {
            bool success = false;
            Users UsersRequestManager = new Users();
            success = UsersRequestManager.CheckIfEmailAlreadyExistInUsersTable(email);
            return success;
        }

        public bool ValidateProperty(string propertyID)
        {
            bool success = false;
            Properties PropertyRequestManager= new Properties();
            success = PropertyRequestManager.CheckIfPropertyExist(propertyID);
            return success;
        }

        public string PostEmail(string toEmailAddress, string messageBody, string messageSubject)
        {
            string success;
            Mails MailRequestManager = new Mails();
            success = MailRequestManager.SendEmail(toEmailAddress, messageBody, messageSubject);
            return success;
        }
        

         


        public String AddPropertyVisit(Visit aPropertyVisit)
        {
            String Success;
            Visits PropertyVisitRequestManager = new();
            Success = PropertyVisitRequestManager.CreatePropertyVisit(aPropertyVisit);
            return Success;
        }

        public List<Visit> RetrieveOpenPropertyVisit()
        {
            List<Visit> OpenVisitList = new();
            Visits VisitRequestManager= new();
            OpenVisitList = VisitRequestManager.GetAllOpenPropertyVisit();
            return OpenVisitList;
        }

        public string UpdateVisitStatus(string findVisitID, string visitStatus)
        {
            String Success;
            Visits PropertyVisitRequestManager = new();
            Success = PropertyVisitRequestManager.ConfirmOrClosePropertyVisit(findVisitID, visitStatus);
            return Success;
        }


    }
}
