using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace CNMaisons.Pages
{
    public class RequestPropertyVisitModel : PageModel
    {


        public string Message { get; set; } = string.Empty;
        public string errorMessage { get; set; } = string.Empty;
        public bool FlagError;

        [BindProperty]
        public string PropertyID { get; set; }
        
        [BindProperty]
        public string FirstName { get; set; }
        
        [BindProperty]
        public string LastName { get; set; }
        
        [BindProperty]
        public string Email { get; set; }
        
        [BindProperty]
        public string PhoneNumber { get; set; }
        
        [BindProperty]
        public DateTime DateRecorded { get; set; }
        
        [BindProperty]
        public DateTime ProposedVisitDate { get; set; }
        
        [BindProperty]
        public TimeSpan ProposedVisitTime { get; set; }
        
        [BindProperty]
        public string VisitStatus { get; set; }

        public void OnPost()
        {
            string errorMessage = "";

            if (string.IsNullOrEmpty(FirstName))
            {
                errorMessage += "The FirstName is Required.\n";
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errorMessage += "The LastName is Required.\n";
            }

            if (string.IsNullOrEmpty(Email))
            {
                errorMessage += "The Email is Required.\n";
            }

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                errorMessage += "The PhoneNumber is Required.\n";
            }

            if (DateRecorded == default(DateTime))
            {
                errorMessage += "The DateRecorded is Required.\n";
            }

            if (ProposedVisitDate == default(DateTime))
            {
                errorMessage += "The ProposedVisitDate is Required.\n";
            }

            if (ProposedVisitTime == default(TimeSpan))
            {
                errorMessage += "The ProposedVisitTime is Required.\n";
            }

            if (string.IsNullOrEmpty(VisitStatus))
            {
                errorMessage += "The VisitStatus is Required.\n";
            }



            if (ModelState.IsValid)
            {
                CNMPMS PropertyVisitRequestDirector = new();

                Visit newVisit = new();
                newVisit.PropertyID = PropertyID;
                newVisit.FirstName = FirstName;
                newVisit.LastName = LastName;
                newVisit.Email = Email;
                newVisit.PhoneNumber = PhoneNumber;
                newVisit.DateRecorded = DateRecorded;
                newVisit.ProposedVisitDate = ProposedVisitDate;
                newVisit.ProposedVisitTime = ProposedVisitTime;
                newVisit.VisitStatus = VisitStatus;



                string UserAccountConfirmation = PropertyVisitRequestDirector.AddPropertyVisit(newVisit);
                if (UserAccountConfirmation == "Successful!")
                {
                    Message = "User Account has been created succesfully.";
                    errorMessage = "";
                }
                else
                {
                    Message = UserAccountConfirmation;
                }

            }
        }
            

        }
}
