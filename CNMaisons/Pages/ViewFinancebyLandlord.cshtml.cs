using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace CNMaisons.Pages
{
    [Authorize(Roles = "LandLord")] // Restrict access to specified roles
    public class ViewFinancebyLandlordModel : PageModel
    {
        public string Message { get; set; } = string.Empty;

        public bool ViewPage;

        [BindProperty]
        public int FindStartYear { get; set; }
        [BindProperty]
        public int FindEndYear { get; set; }
        public List<Payment> FinancialRecordList = new List<Payment>();
        CNMPMS FinancialRequestDirector = new CNMPMS();
        public List<Payment> FinancialRecordDSueSixMonthsList = new List<Payment>();
        public List<Payment> FinancialRecordDSueThreeMonthsList = new List<Payment>();
        public User Users { get; set; } = new User();
        public string Email { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
        public void OnGet()
        {
            CNMPMS tenantController = new();
            if (HttpContext.Session.GetString("Email") != null)
            {
                Email = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(Email);
            Employee = tenantController.GetAllEmployees(Email);
        }
        public void OnPost()
        {
            OnGet();
            ViewPage = false;

            ModelState.Clear();
            Message = "";

            if (FindStartYear == 0)
            {
                ModelState.AddModelError("FindStartYear", "FindStartYear is Required.");
            }


            if (FindEndYear == 0)
            {
                ModelState.AddModelError("FindStartYear", "FindStartYear is Required.");
            }



            if (FindEndYear < FindStartYear)
            {
                int tempData = FindEndYear;
                FindEndYear = FindStartYear;
                FindStartYear = tempData;
            }

            if (ModelState.IsValid)
            {

                
                FinancialRecordList = FinancialRequestDirector.ViewFinancialRecordbyDate(FindStartYear, FindEndYear);
                if (FinancialRecordList != null)
                {

                 
                    FinancialRecordDSueSixMonthsList = FinancialRequestDirector.GetFinancialRecordDueInSixMonths();
                    FinancialRecordDSueThreeMonthsList = FinancialRequestDirector.GetFinancialRecordDueInThreeMonths();

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
