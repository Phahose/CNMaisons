using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Identity;

namespace CNMaisons.Controller
{
    public class CNMS
    {
        public bool AddProperty(Property property)
        {
            bool success = false;
            Properties RequestManager = new Properties();
            success = RequestManager.AddProperty(property);
            return success;
        }
        public bool UpdateProperty(Property property)
        {
            bool success = false;
            Properties RequestManager = new Properties();
            success = RequestManager.UpdateProperty(property);
            return success;
        }

        public bool DeleteProperty(string propertyID)
        {
            bool success = false;
            Properties RequestManager = new Properties();
            success = RequestManager.DeleteProperty(propertyID);
            return success;
        }
        public List<Property> GetProperties()
        {
            List<Property> PropertyList = new List<Property>();
            Properties RequestManager = new Properties();
            PropertyList = RequestManager.GetProperty();
            return PropertyList;
        }
        public Property GetPropertyByID(string propertyID)
        {
            Property Property = new();
            Properties RequestManager = new Properties();
            Property = RequestManager.GetPropertyByID(propertyID);
            return Property;
        }

        public String CreateUserAccount(User aUserAccount)
        {

            String Success;
            Users RequestManager = new();
            Success = RequestManager.AddUser(aUserAccount);
            return Success;
        }

        public User GetUserByEmail(string existingUseremail)
        {
            User user = new User();
            Users RequestManager = new Users();
            user = RequestManager.GetUserByEmail(existingUseremail);
            return user;
        }


        public string SubmitLeaseApplication(Tenant aTenant)
        {
            string Success;
            Tenants RequestManager = new();
            Success = RequestManager.AddLeaseApplication(aTenant);
            return Success;
        }
        public List<Tenant> GetPendingLeaseApplication()
        {
            List<Tenant> TenantsPendingApplicationReview = new();
            Tenants RequestManager = new();
            TenantsPendingApplicationReview = RequestManager.RetrievePendingLeaseApplication();
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

}
}
