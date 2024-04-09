using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    [Authorize(Roles = "LandLord, Staff")]   // Restrict access to specified roles
    public class ViewTenantbyLandLordModel : PageModel
    {
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public CNMPMS RequestDirector { get; set; } = new();
        public Tenant aTenant = new();
        [BindProperty]
        public string FindTenantID { get; set; } = string.Empty;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;
        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();
        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;
        public User Users { get; set; } = new User();
        public string Email { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();

        public void OnGet()
        {
            CNMPMS tenantController = new();
            ListOfTenantsPendingReview = tenantController.GetPendingLeaseApplication();
            if (ListOfTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";
            }
            if (HttpContext.Session.GetString("Email") != null)
            {
                Email = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(Email);
            Employee = tenantController.GetAllEmployees(Email);
        }
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                Email = HttpContext.Session.GetString("Email")!;
            }
            Users = RequestDirector.GetUserByEmail(Email);
            Employee = RequestDirector.GetAllEmployees(Email);


            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("IndexStaff");

                case "Find":
                    if (FindTenantID != null)
                    {
                        ViewFormNow = false;
                        if (ModelState.IsValid)
                        {
                            SetSessionString("FindTenantID1", FindTenantID);  // save content for furtre retreival
                            ViewFormNow = false;
                            CNMPMS RequestDirector = new();
                            aTenant = RequestDirector.ViewTenant(FindTenantID);
                            string check = aTenant.TenantID;
                            if (check != "")
                            {
                                ViewFormNow = true;
                                Message = "Below are the detail of the Tenant's Lease application.";
                                return Page();
                            }
                            else
                            {
                                Message = "This record does not exist.";
                                ViewFormNow = false;
                            }
                        }
                    }
                    return Page();
            }
            return Page();
        }
        void SetSessionString(string key, string value)
        {
            if (value != null)
            {
                HttpContext.Session.SetString(key, value);
            }
            else
            {
                HttpContext.Session.SetString(key, "");
            }
        }

    }
}