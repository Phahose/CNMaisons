using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string AlertClass { get; set; } = string.Empty;
        public void OnGet()
        {
        }
    }
}
