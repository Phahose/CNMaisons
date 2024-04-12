using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class AddPaymentModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public string errorMessage { get; set; } = string.Empty;
        public bool FlagError;

        [BindProperty]
        public int PaymentID { get; set; }
        [BindProperty] 
        public string TenantID { get; set; }
        [BindProperty] 
        public string PropertyID { get; set; }
        [BindProperty] 
        public decimal AmountPaid { get; set; }
        [BindProperty] 
        public string PaymentStartMonth { get; set; }
        [BindProperty]
        public string PaymentEndMonth { get; set; }

        [BindProperty] 
        public int PaymentStartYear { get; set; }
        [BindProperty] 
        public int MonthsPaidFor { get; set; }
        [BindProperty] 
        public int NextDueMonth { get; set; }
        [BindProperty] 
        public int NextDueYear { get; set; }
        [BindProperty] 
        public DateTime NextDueDate { get; set; }
        [BindProperty] 
        public DateTime DateOfTenantsPayment { get; set; }
        [BindProperty] 
        public string MethodOfPayment { get; set; }
        [BindProperty] 
        public DateTime TenantPaymentBank { get; set; }
        [BindProperty] 
        public DateTime DateOfRecord { get; set; }
        public int[] month1 = { 1, 2, 3,  4, 5, 6, 7, 8, 9, 10, 11, 12};
        public int[] month2 = {12, 1, 2,  3, 4, 5, 6, 7, 8,  9, 10, 11};




    }
}
