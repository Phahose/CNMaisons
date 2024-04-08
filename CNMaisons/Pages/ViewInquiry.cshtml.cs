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
        public CNMPMS PropertyVisitRequestDirector;
 
        [BindProperty]
        public string FindVisitID { get; set; } = string.Empty;


        [BindProperty]
        public string Submit { get; set; } = string.Empty;

         public List<Visit> OpenVisitList = new List<Visit>();

        [BindProperty]
        public string VisitStatus { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;
        public bool ShowForm = false;
        public void OnGet()
        {
              
            CNMPMS VisitRequestDirector= new CNMPMS();
            OpenVisitList = VisitRequestDirector.RetrieveOpenPropertyVisit();
            if (OpenVisitList == null)
            {
                Message = "All have been reviewed";
            }
        }
        public IActionResult OnPost()
        {
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("/Index");
                    break;


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
                            
                            CNMPMS VisitRequestDirector = new CNMPMS();
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
                    break;

                 
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
