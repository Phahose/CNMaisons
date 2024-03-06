using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class IndexModel : PageModel
    {
        public List<Property> PropertyList { get; set; } = new();
        public List<Property> DisplayedPropertyList { get; set; } = new();
        public void OnGet()
        {
            BCS controller = new BCS();
            PropertyList = controller.GetProperties();
            DisplayedPropertyList = PropertyList.Take(3).ToList();
        }
    }
}
