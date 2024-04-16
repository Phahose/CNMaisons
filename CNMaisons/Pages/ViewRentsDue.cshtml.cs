using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    //[Authorize(Roles = "LandLord")] // Restrict access to specified roles

    public class ViewRentsDueModel : PageModel
    {

        CNMPMS FinancialRequestDirector = new CNMPMS();
        public List<Payment> FinancialRecordDSueSixMonthsList = new List<Payment>();
        public List<Payment> FinancialRecordDSueThreeMonthsList = new List<Payment>();

        public void OnGet()
        {
            FinancialRecordDSueSixMonthsList = FinancialRequestDirector.GetFinancialRecordDueInSixMonths();
            FinancialRecordDSueThreeMonthsList = FinancialRequestDirector.GetFinancialRecordDueInThreeMonths();

        }
    }
}
