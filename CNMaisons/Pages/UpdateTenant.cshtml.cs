using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CNMaisons.Pages
{
    public class UpdateTenantModel : PageModel
    {
        public bool ViewFormNow = false;
        public string Message { get; set; } = string.Empty;
        public CNMPMS RequestDirector;

        public Tenant tenantForReview = new();

        [BindProperty]
        public string FindTenantID { get; set; } = string.Empty;


        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        public string ListMessage { get; set; } = string.Empty;
        public bool ShowForm = false;

        public List<Tenant> ListOfTenantsPendingReview = new List<Tenant>();
        public string errorMessage { get; set; } = string.Empty;

        [BindProperty]
        public IFormFile Passport { get; set; }

        [BindProperty]
        public string TenantID { get; set; } = string.Empty;


        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public DateTime DOB { get; set; }

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string Nationality { get; set; } = string.Empty;

        [BindProperty]
        public string StateofOrigin { get; set; } = string.Empty;

        [BindProperty]
        public string LGA { get; set; } = string.Empty;

        [BindProperty]
        public string HomeTown { get; set; } = string.Empty;

        [BindProperty]
        public string PermanentHomeAddress { get; set; } = string.Empty;

        [BindProperty]
        public string Occupation { get; set; } = string.Empty;

        [BindProperty]
        public string SelfEmployed { get; set; } = string.Empty;

        [BindProperty]
        public string BusinessRegistrationNumber { get; set; } = string.Empty;

        [BindProperty]
        public byte[] CoporateAffairsCertificate { get; set; } = new byte[0];

        [BindProperty]
        public string NameofEmployer { get; set; } = string.Empty;

        [BindProperty]
        public string AddressOfEmployer { get; set; } = string.Empty;

        [BindProperty]
        public int LengthOnJob { get; set; }

        [BindProperty]
        public string CurrentPositionHeld { get; set; } = string.Empty;

        [BindProperty]
        public string NatureOfJob { get; set; } = string.Empty;

        [BindProperty]
        public string FormerResidenceAddress { get; set; } = string.Empty;

        [BindProperty]
        public string ReasonForMoving { get; set; } = string.Empty;

        [BindProperty]
        public int LengthOfStayAtOldResidence { get; set; }

        [BindProperty]
        public string NameOfFormerResidentManager { get; set; } = string.Empty;

        [BindProperty]
        public string ObjectionsToReasonsForMoving { get; set; } = string.Empty;

        [BindProperty]
        public string MaritalStatus { get; set; } = string.Empty;

        [BindProperty]
        public string SpouseFirstName { get; set; } = string.Empty;

        [BindProperty]
        public string SpouseLastName { get; set; } = string.Empty;

        [BindProperty]
        public string SpouseOccupation { get; set; } = string.Empty;

        [BindProperty]
        public int NumberOfOccupants { get; set; }

        [BindProperty]
        public string NextOfKinFirstName { get; set; } = string.Empty;

        [BindProperty]
        public string NextOfKinLastName { get; set; } = string.Empty;

        [BindProperty]
        public string NextOfKinAddress { get; set; } = string.Empty;

        [BindProperty]
        public string NextOfKinPhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor1FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor1LastName { get; set; } = string.Empty;


        [BindProperty]
        public string Guarantor1Address { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor1Occupation { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor1PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor1AlternatePhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor2FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor2LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor2Address { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor2Occupation { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor2PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Guarantor2AlternatePhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        public string Declaration { get; set; } = string.Empty;

        [BindProperty]
        public string YourSignature { get; set; } = string.Empty;


        [BindProperty]
        public bool DeleteFlag { get; set; }


        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;







        public void OnGet()
        {
            CNMPMS tenantController = new();
            ListOfTenantsPendingReview = tenantController.GetPendingLeaseApplication();
            if (ListOfTenantsPendingReview == null)
            {
                ListMessage = "All have been reviewed";
            }
        }
        public IActionResult OnPost()
        {
            ModelState.Clear();
            Message = "";

            switch (Submit)
            {
                case "Close":
                    return RedirectToPage("/Index");
                    break;

                case "Find":
                    if (FindTenantID != null)
                    {
                        ViewFormNow = false;
                        if (ModelState.IsValid)
                        {
                            SetSessionString("FindTenantID1", FindTenantID);  // save content for furtre retreival

                            CNMPMS RequestDirector = new();
                            tenantForReview = RequestDirector.ViewTenant(FindTenantID);
                            if (tenantForReview != null)
                            {
                                ViewFormNow = true;
                                Message = "Below are the detail of the Tenant's Lease application.";
                                return Page();
                            }
                        }
                    }
                    return Page();
                    break;

                case "Update Tenant":
                    if (ModelState.IsValid)
                    {


                    }
                    return Page();
                    break;
            }
            return Page();
        }
        void SetSessionString(string key, string value)
        {
            if (value != null)
            {
                HttpContext.Session.SetString(key, value);
            }
            else
            {
                HttpContext.Session.SetString(key, "");
            }
        }


    }
}
