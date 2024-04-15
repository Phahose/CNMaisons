using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    [Authorize(Roles = "LandLord, Staff")]
    public class ViewPaymentModel : PageModel
    {
        public string Message { get; set; } = string.Empty;

        public bool ViewPage ;

        [BindProperty]
        public string FindTenantID { get; set; } = string.Empty;

        [BindProperty]
        public DateTime FindStartDate { get; set; } 

        [BindProperty]
        public DateTime FindEndDate { get; set; } 

        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        public List<Payment> PaymentList = new List<Payment>();
        public string UserEmail { get; set; } = string.Empty;
        public User Users { get; set; } = new User();
        public Employee Employee { get; set; } = new Employee();
        public void OnGet()
        {
            CNMPMS tenantController = new();
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(UserEmail);
            Employee = tenantController.GetAllEmployees(UserEmail);
        }

        public void OnPost()
        {
            CNMPMS PaymentRequestDirector = new CNMPMS();
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = PaymentRequestDirector.GetUserByEmail(UserEmail);
            Employee = PaymentRequestDirector.GetAllEmployees(UserEmail);
            
            ViewPage = false;
            ModelState.Clear();
            Message = "";

            if (string.IsNullOrEmpty(FindTenantID))
            {
                ModelState.AddModelError("FindTenantID", "TenantID is Required.");
            }


            // Validate DOB
            if (FindStartDate == DateTime.MinValue)
            {
                ModelState.AddModelError("FindStartDate", "Start Date is Required");
            }

            // Validate DOB
            if (FindEndDate == DateTime.MinValue)
            {
                ModelState.AddModelError("FindEndDate", "End Date is Required");
            }

            if (ModelState.IsValid)
            {  
                PaymentList = PaymentRequestDirector.ViewPaymentbyDate(FindTenantID, FindStartDate, FindEndDate);
                if (PaymentList != null)
                {
                    Message = "Below are your records.";
                    ViewPage = true;
                }
                else
                { 
                    ViewPage = false; 
                }
            }

        }
    }
}
