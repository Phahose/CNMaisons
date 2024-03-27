using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Controller;
using CNMaisons.Domain;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace CNMaisons.Pages
{
    public class RequestMaintenanceModel : PageModel
    {
        [BindProperty]
        public int MaintenanceId { get; set; } 
        [BindProperty]
        public string TenantID { get; set;} = string.Empty ;
        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;
        [BindProperty]
        public DateOnly StartDate { get; set; }
        [BindProperty]
        public DateOnly EndDate { get; set; }
        [BindProperty]
        public string MaintenanceType { get; set; } = string.Empty;
        [BindProperty]
        public string Description { get; set; } = string.Empty;
        [BindProperty]
        public string Priority { get; set; } = string.Empty;
        [BindProperty]
        public IFormFile Image1 { get; set; }
        [BindProperty]
        public IFormFile Image2 { get; set; }
        [BindProperty]
        public string Remarks { get; set; } = string.Empty;
        [BindProperty]
        public DateOnly CompletionDate { get; set;}
        [BindProperty]
        public DateOnly ApprovedStartDate { get; set; }
        [BindProperty]
        public decimal ActualCost { get; set; }
        [BindProperty]
        public string Status { get; set; } = string.Empty;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        [BindProperty]
        public string SuccessMessage { get; set; } = string.Empty;
        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
        }
        public void OnPost() 
        {
            if (TenantID == null)
            {
                ModelState.AddModelError("TenantIDError", "Tenant ID is Required");
            }
            if (PropertyID == null)
            {
                ModelState.AddModelError("PropertyIDError", "Property ID is Required");
            }
            if (StartDate == null)
            {
                ModelState.AddModelError("StartDateError", "Start Date is Required");
            }
            if (EndDate == null)
            {
                ModelState.AddModelError("EndDateError", "End Date is Required");
            }
            if (MaintenanceType == null)
            {
                ModelState.AddModelError("MaintenanceType", "Maintenance Type is Required");
            }
            if (Description == null)
            {
                ModelState.AddModelError("Description", "Description is Required");
            }
            if (Priority == null)
            {
                ModelState.AddModelError("MaintenanceType", "Maintenance Type is Required");
            }
            if (Image1 == null)
            {
                ModelState.AddModelError("Image1Error", "Image is Required");
            }
            if (Image2 == null)
            {
                ModelState.AddModelError("Image2Error", "Image is Required");
            }

            if (ModelState.IsValid)
            {
               
            }
        }
    }
}
