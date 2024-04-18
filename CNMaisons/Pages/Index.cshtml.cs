using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
            PropertyList = GetListProperties();
            DisplayedPropertyList = PropertyList.Where(p => p.Occupied == false).ToList();
        }

        public IActionResult OnPost()
        {
            CNMPMS controller = new CNMPMS();
            PropertyList = GetListProperties();
            DisplayedPropertyList = PropertyList.Where(p => p.Occupied == false).ToList();
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (FormType == "SearchForm")
                {
                    if (StateFilter != null)
                    {
                        DisplayedPropertyList = DisplayedPropertyList.Where(p => p.PropertyLocationState == StateFilter).ToList();
                        if (DisplayedPropertyList.Count < 1)
                        {
                            ErrorMessage = "No Properties Match the Set parameters";
                        }

                        return Page();
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

        public List<Property> GetListProperties()
        {
            List<Property> properties = new List<Property>();
            string propertyliststring;
            if (HttpContext.Session.GetString("ListOfProperties") == null)
            {
                CNMPMS controller = new CNMPMS();
                properties = controller.GetProperties();

                propertyliststring = JsonSerializer.Serialize(properties);
                HttpContext.Session.SetString("ListOfProperties", propertyliststring);
            }
            else
            {
                if (HttpContext.Session.GetString("PropertyHasBeenupdated") == "True")
                {
                    CNMPMS controller = new CNMPMS();
                    properties = controller.GetProperties();

                    propertyliststring = JsonSerializer.Serialize(properties);
                    HttpContext.Session.SetString("ListOfProperties", propertyliststring);
                    HttpContext.Session.SetString("PropertyHasBeenupdated", "False");
                }
                else
                {
                    propertyliststring = HttpContext.Session.GetString("ListOfProperties")!;
                    properties = JsonSerializer.Deserialize<List<Property>>(propertyliststring)!;
                }
            }
            return properties;
        }
    }
}