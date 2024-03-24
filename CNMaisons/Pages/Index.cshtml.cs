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
        public void OnGet()
        {
            CNMS controller = new CNMS();
            PropertyList = controller.GetProperties();
            DisplayedPropertyList = PropertyList.ToList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (FormType == "SearchForm")
                {

                }
                else if (FormType == "InfoForm")
                {
                    HttpContext.Session.Clear();
                    CNMS controller = new CNMS();
                    if (PropertyID != null)
                    {
                        HttpContext.Session.SetString("PropertyID", PropertyID);
                    }
                }      
            }
            return RedirectToPage("/PropertyDetails");
        }
    }
}
