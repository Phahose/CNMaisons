using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{

    [Authorize(Roles = "LandLord, Staff")]   // Restrict access to specified roles

    public class ViewInquiryModel : PageModel
    {
        public Visit ListOfPendingVisits = new();
        public string Message { get; set; } = string.Empty;
        public CNMPMS PropertyVisitRequestDirector = new();
 
        [BindProperty]
        public string FindVisitID { get; set; } = string.Empty;
        public User Users { get; set; } = new User();

        [BindProperty]
        public string Submit { get; set; } = string.Empty;

         public List<Visit> OpenVisitList = new List<Visit>();

        [BindProperty]
        public string VisitStatus { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;
        public bool ShowForm = false;
        public Employee Employee { get; set; } = new Employee();
        public void OnGet()
        {
              
            CNMPMS VisitRequestDirector= new CNMPMS();
            OpenVisitList = VisitRequestDirector.RetrieveOpenPropertyVisit();
            OpenVisitList = OpenVisitList.Where(x => x.VisitStatus != "Completed").ToList();
            OpenVisitList = OpenVisitList.Where(x => x.VisitStatus != "Cancelled").ToList();
            if (OpenVisitList == null)
            {
                Message = "All have been reviewed";
            }
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = VisitRequestDirector.GetUserByEmail(UserEmail);
            Employee = VisitRequestDirector.GetAllEmployees(UserEmail);
        }
        public IActionResult OnPost()
        {
            CNMPMS VisitRequestDirector = new CNMPMS();
            OpenVisitList = VisitRequestDirector.RetrieveOpenPropertyVisit();
            OpenVisitList = OpenVisitList.Where(x => x.VisitStatus != "Completed").ToList();
            OpenVisitList = OpenVisitList.Where(x => x.VisitStatus != "Cancelled").ToList();
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = VisitRequestDirector.GetUserByEmail(UserEmail);
            Employee = VisitRequestDirector.GetAllEmployees(UserEmail);
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                return RedirectToPage("/IndexStaff");



                case "Refresh":
                    OnGet();
                break;

                case "Submit":
                    if (FindVisitID != null)
                    {
                    

                        if (string.IsNullOrEmpty(FindVisitID))
                        {
                            ModelState.AddModelError("FindVisitID", "VisitID is Required.");
                        }


                        if (ModelState.IsValid)
                        {
                            
                            
                            string Confirmation = VisitRequestDirector.UpdateVisitStatus(FindVisitID, VisitStatus)
;
                            if (Confirmation == "Successful!")
                            {

                                string messageBody = "Hello " + FirstName + ",\n\nYour visit request is now confirmed.\n\nRegards\n\nCNMaisons Mgt.";
                                string messageSubject = "Date for Property Visit Confirmed.";

                                string mailConfirmation;
                                CNMPMS MailRequestManager = new CNMPMS();
                                mailConfirmation = MailRequestManager.PostEmail(Email, messageBody, messageSubject);
                                
                                Message = "Status updated and mail sent to client.";
                                OnGet(); 
                                return Page();
                            }
                        }
                    }
                    OnGet(); 
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
