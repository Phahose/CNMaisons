using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class UpdatePropertyModel : PageModel
    {
        public string ErrorMessage {get; set; } = string.Empty;
        public string SucceessMessage { get; set; } = string.Empty;
        public string PropertyID { get; set; } = string.Empty;
        public Property Property { get; set; } = new();
        public void OnGet()
        {
            if (HttpContext.Session.GetString("PropertyID") != null)
            {
                PropertyID = HttpContext.Session.GetString("PropertyID")!;
            }

            BCS controller = new BCS();
            Property = controller.GetPropertyByID(PropertyID);
        }
    }
}
