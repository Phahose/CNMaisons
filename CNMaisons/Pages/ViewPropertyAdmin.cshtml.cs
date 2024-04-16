using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CNMaisons.Pages
{
    [Authorize]
    public class ViewPropertyAdminModel : PageModel
    {
        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;
        [BindProperty]
        public string DeletedPropertyID { get; set; } = string.Empty;
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
        public User Users { get; set; } = new User();
        public string Email { get; set; } = string.Empty;
        public Employee Employee { get; set; } = new Employee();
        public void OnGet()
        {
            Email = HttpContext.Session.GetString("Email")!;
            CNMPMS controller = new CNMPMS();
            PropertyList = GetListProperties();
            DisplayedPropertyList = PropertyList.ToList();
            Users = controller.GetUserByEmail(Email);
            Employee = controller.GetAllEmployees(Email);
        }

        public IActionResult OnPost()
        {
            Email = HttpContext.Session.GetString("Email")!;
            CNMPMS controller = new CNMPMS();
            PropertyList = GetListProperties();
            DisplayedPropertyList = PropertyList.ToList();
            Users = controller.GetUserByEmail(Email);
            Employee = controller.GetAllEmployees(Email);
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
                    controller = new CNMPMS();
                    if (PropertyID != null)
                    {
                        HttpContext.Session.SetString("PropertyID", PropertyID);
                    }
                    return RedirectToPage("/UpdateProperty");
                case "Delete":
                    //PropertyID = HttpContext.Session.GetString("PropId")!;
                    Success = controller.DeleteProperty(DeletedPropertyID);
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
