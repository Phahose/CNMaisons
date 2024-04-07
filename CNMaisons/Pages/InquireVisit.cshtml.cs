//using CNMaisons.Controller;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using CNMaisons.Domain;
//using System.Security.Cryptography.X509Certificates;
//using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
//using CNMaisons.TechnicalService;
//using Microsoft.AspNetCore.Mvc.Razor;

//namespace CNMaisons.Pages
//{
//    public class InquireVisitModel : PageModel
//    {
//        public Visit ListOfPendingVisits = new Visit();
//        public string Message { get; set; } = string.Empty;
//        public CNMPMS PropertyVisitRequestDirector;

//        public bool ViewFormNow { set; get; }

//        [BindProperty]
//        public string FindTenantID { get; set; } = string.Empty;


//        [BindProperty]
//        public string Submit { get; set; } = string.Empty;

//        public string ListMessage { get; set; } = string.Empty;
//        public bool ShowForm = false;
//        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();

//        [BindProperty]
//        public string ApprovalStatus { get; set; } = string.Empty;


//        public void OnGet()
//        {
//            CNMPMS tenantController = new();
//            ListOfTenantsPendingReview = tenantController.GetPendingLeaseApplication();
//            if (ListOfTenantsPendingReview == null)
//            {
//                ListMessage = "All have been reviewed";
//            }
//        }
//        public IActionResult OnPost()
//        {
//            ModelState.Clear();
//            Message = "";

//            switch (Submit)
//            {
//                case "Close":
//                    return RedirectToPage("/Index");
//                    break;

//                case "Find":
//                    if (FindTenantID != null)
//                    {
//                        ViewFormNow = false;
//                        if (ModelState.IsValid)
//                        {
//                            SetSessionString("FindTenantID1", FindTenantID);  // save content for furtre retreival

//                            CNMPMS VisitRequestDirector = new CNMPMS();
//                            Tenant tenantForReview = VisitRequestDirector.ViewTenant(FindTenantID);
//                            if (tenantForReview != null)
//                            {
//                                ViewFormNow = true;
//                                Message = "Below are the detail of the Tenant's Lease application.";
//                                return Page();
//                            }
//                        }
//                    }
//                    return Page();
//                    break;

//                case "Submit Review":
//                    if (ApprovalStatus != "Pending")
//                    {
//                        ViewFormNow = false;
//                        if (ModelState.IsValid)
//                        {
//                            FindTenantID = HttpContext.Session.GetString("FindTenantID1") ?? string.Empty;

//                            CNMPMS RequestDirector = new();
//                            String Confirmation = RequestDirector.ReviewAApprovedApplication(FindTenantID, ApprovalStatus, lease);
//                            if (Confirmation == "Successful!")
//                            {
//                                ViewFormNow = false;
//                                Message = "Tenant's Lease application reviewed.";
//                                OnGet();
//                                return Page();
//                            }
//                        }


//                    }
//                    return Page();
//                    break;
//            }
//            return Page();
//        }
//        void SetSessionString(string key, string value)
//        {
//            if (value != null)
//            {
//                HttpContext.Session.SetString(key, value);
//            }
//            else
//            {
//                HttpContext.Session.SetString(key, "");
//            }
//        }
//    }
//}
