using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace CNMaisons.Pages
{
    [AllowAnonymous] 

    public class RequestPropertyVisitModel : PageModel
    {


        public string Message { get; set; } = string.Empty;
        public string errorMessage { get; set; } = string.Empty;
        public bool FlagError;

        public string MessageForFile { get; set; } = string.Empty;
        public CNMPMS PropertyRequestDirector { get; set; } = new();
        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string PhoneNumber { get; set; } = string.Empty;



        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime ProposedVisitDate { get; set; } = DateTime.Now;

        [BindProperty]
        public TimeSpan ProposedVisitTime { get; set; } = DateTime.Now.TimeOfDay;

        [BindProperty]
        public string VisitStatus { get; set; } = string.Empty;

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

            if (string.IsNullOrEmpty(PropertyID))
            {
                errorMessage += "PropertyID is Required.\n";
            }
            else
            {
                bool propertyIDConfirmation = PropertyRequestDirector.ValidateProperty(PropertyID);

                if (propertyIDConfirmation == false)
                {
                    errorMessage += "This is not a valid PropertyID.\n";
                }
            }



            // Check if any error occurred and add it to the model state
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("AllError", errorMessage);
                Message = errorMessage;
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
                newVisit.ProposedVisitDate = ProposedVisitDate;
                newVisit.ProposedVisitTime = ProposedVisitTime;
                newVisit.VisitStatus = VisitStatus;



                string UserAccountConfirmation = PropertyVisitRequestDirector.AddPropertyVisit(newVisit);
                if (UserAccountConfirmation == "Successful!")
                {
                    Message = "Visit Booked SuccessFully";
                    errorMessage = "";
                    List<User> LandLords = new List<User>();
                    LandLords = PropertyVisitRequestDirector.GetActiveUsers();
                    LandLords = LandLords.Where(x => x.Role == "LandLord" || x.Role== "Staff").ToList();
                    CNMPMS MailRequestManager = new CNMPMS();
                    foreach (var landlord in LandLords)
                    {
                        string messageBody = "Hello,\n" +
                                          "\nYou have a request to view a property at CN Maisons \n" +
                                          "\tPersons Name: " + FirstName +" "+ LastName +
                                          "\n\tProperty ID: " + PropertyID +
                                          "\n\tProposed Visit Date: " + ProposedVisitDate.ToString("dddd MMMM dd, yyyy.") +
                                          "\n\tProposed Visit Time: " + ProposedVisitTime +
                                          "\nLogin To Your Account on CN Maisons to Review this Visit";
                        string messageSubject = "New Property Visit Request";

                        string mailConfirmation;

                        mailConfirmation = MailRequestManager.PostEmail("ekwomnick@yahoo.com", messageBody, messageSubject);
                    }
                }
                else
                {
                    Message = UserAccountConfirmation;
                }

            }
        }


    }
}
