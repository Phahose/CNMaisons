using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CNMaisons.Domain;
using CNMaisons.Controller;

namespace CNMaisons.Pages
{
    public class PropertyDetailsModel : PageModel
    {
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
