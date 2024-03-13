using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class ViewPropertyAdminModel : PageModel
    {
        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;
        [BindProperty]
        public string PropertyIDFilter { get; set; } = string.Empty;
        public List<Property> PropertyList { get; set; } = new();
        public List<Property> DisplayedPropertyList { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        [BindProperty]
        public string Submit {  get; set; } = string.Empty;
        [BindProperty]
        public string StateFilter {  get; set; } = string.Empty;
        public string Message {  get; set; } = string.Empty;
        public bool DeleteRequest { get; set; } = false;
        public bool Success { get; set; } = false;  
       
        public void OnGet()
        {
            BCS controller = new BCS();
            PropertyList = controller.GetProperties();
            DisplayedPropertyList = PropertyList.ToList();
        }

        public IActionResult OnPost()
        {
            BCS controller = new BCS();
            PropertyList = controller.GetProperties();
            DisplayedPropertyList = PropertyList.ToList();
            switch (Submit)
            {
                case "Filter":
                    if (PropertyIDFilter != null || StateFilter != null)
                    {
                        if (PropertyIDFilter != null)
                        {
                            DisplayedPropertyList = DisplayedPropertyList.Where(p => p.PropertyID == PropertyIDFilter).ToList();
                        }
                        if (StateFilter != null && PropertyIDFilter == null)
                        {
                            DisplayedPropertyList = DisplayedPropertyList.Where(p => p.PropertyLocationState == StateFilter).ToList();
                        }

                        if (DisplayedPropertyList.Count == 0)
                        {
                            Message = "No Properties Match the Set parameters";
                        }
                    }
                    else
                    {
                        Message = "No Properties Match the Set parameters";
                    }
                    return Page();
            
                case "Update Property":
                    HttpContext.Session.Clear();
                    controller = new BCS();
                    if (PropertyID != null)
                    {
                        HttpContext.Session.SetString("PropertyID", PropertyID);
                    }
                    return RedirectToPage("/UpdateProperty");
                case "Delete Property":
                    DeleteRequest = true;
                    HttpContext.Session.SetString("PropId", PropertyID);
                break;
                case "Cancel":
                    DeleteRequest = false;
                break;
                case "Delete":
                    PropertyID = HttpContext.Session.GetString("PropId")!;
                    Success = controller.DeleteProperty(PropertyID);
                    if (Success)
                    {
                        SuccessMessage = "Property Deleted SuccessFully";
                    }
                    else
                    {
                        ErrorMessage = "Could Not Delete Property Error Occured";
                    }
                break;

               
            }
            return Page();
        }
    }
}
