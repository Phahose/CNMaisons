using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;
        [BindProperty]
        public string FormType { get; set; } = string.Empty;
        public List<Property> PropertyList { get; set; } = new();
        public List<Property> DisplayedPropertyList { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        [BindProperty]
        public string StateFilter { get; set; } = string.Empty;
        public void OnGet()
        {
            CNMPMS controller = new CNMPMS();
            PropertyList = controller.GetProperties();
            DisplayedPropertyList = PropertyList.Where(p => p.Occupied == false).ToList();
        }

        public IActionResult OnPost()
        {
            CNMPMS controller = new CNMPMS();
            PropertyList = controller.GetProperties();
            DisplayedPropertyList = PropertyList.Where(p => p.Occupied == false).ToList();
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (FormType == "SearchForm")
                {
                    if (StateFilter !=null)
                    {
                        DisplayedPropertyList = DisplayedPropertyList.Where(p => p.PropertyLocationState == StateFilter).ToList();

                        return Page();
                    }

                    if (DisplayedPropertyList.Count < 1)
                    {
                        ErrorMessage = "No Properties Match the Set parameters";
                    }
                }
                else if (FormType == "InfoForm")
                {
                    HttpContext.Session.Clear();
                    if (PropertyID != null)
                    {
                        HttpContext.Session.SetString("PropertyID", PropertyID);
                    }
                    return RedirectToPage("/PropertyDetails");
                }      
            }
            return Page();
        }
    }
}
