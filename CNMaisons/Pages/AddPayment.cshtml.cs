using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;

namespace CNMaisons.Pages
{
    [Authorize(Roles = "LandLord, Staff, Tenant")]   // Restrict access to specified roles
    public class AddPaymentModel : PageModel
    {

        [BindProperty]
        public string TenantID { get; set; } = string.Empty;


        [BindProperty]
        public string PropertyID { get; set; } = string.Empty;


        [BindProperty]
        public decimal AmountPaid { get; set; }

        [BindProperty]
        public string PaymentStartMonth { get; set; }= string.Empty;

        [BindProperty]
        public string PaymentEndMonth { get; set; } = string.Empty;

        [BindProperty]
        public int PaymentStartYear { get; set; }  

        [BindProperty]
        public int MonthsPaidFor { get; set; }

        [BindProperty]
        public string NextDueMonth { get; set; } = string.Empty;
        [BindProperty]
        public int NextDueYear { get; set; }

        [BindProperty]
        public DateTime NextDueDate { get; set; }
        [BindProperty]
        public DateTime DateOfTenantsPayment { get; set; }
        [BindProperty]
        public string MethodOfPayment { get; set; } = string.Empty;

        [BindProperty]
        public string TenantPaymentBank { get; set; } = string.Empty;

        [BindProperty]
        public DateTime DateOfRecord { get; set; }

        CNMPMS PropertyRequestDirector = new CNMPMS();
        Payments PaymentRequestDirector = new Payments();
        Tenant tenantForPayment = new Tenant();
        public string Message { get; set; } = string.Empty;
        public List<string> errorMessage { get; set; } = new();
        public bool FlagError;



        public string Email;
        public string FName;


 
        public void OnPost()
        {
            CNMPMS tenantController = new();
            if (HttpContext.Session.GetString("Email") != null)
            {
                UserEmail = HttpContext.Session.GetString("Email")!;
            }
            Users = tenantController.GetUserByEmail(UserEmail);
            tenantForPayment = tenantController.GetAllTennants(UserEmail);

            #region All Validations
            //ModelState.Clear();


            if (string.IsNullOrEmpty(PropertyID))
            {
                errorMessage.Add("PropertyID is Required");
            }
            else
            {
                bool propertyIDConfirmation = PropertyRequestDirector.ValidateProperty(PropertyID);

                if (propertyIDConfirmation == false)
                {
                    errorMessage.Add("This is not a valid PropertyID");
                }
            }


            if (string.IsNullOrEmpty(TenantID))
            {
                errorMessage.Add("TenantID is Required");
            }
            else
            {
                CNMPMS RequestDirector = new();
                tenantForPayment = RequestDirector.ViewTenant(TenantID);
                Email = tenantForPayment.Email;
                FName = tenantForPayment.FirstName;

                if (tenantForPayment == null)
                {
                    errorMessage.Add("TenantID  is not a valid PropertyID");
                }
            }



            if (AmountPaid <= 0)
            {
                errorMessage.Add("Amount Paid cannot be 0.");
            }

            if (string.IsNullOrEmpty(PaymentStartMonth))
            {
                errorMessage.Add("Payment Start Month is Required.");
            }
            if (string.IsNullOrEmpty(PaymentEndMonth))
            {
                errorMessage.Add("Payment End Month is Required.");
            }
            if (PaymentStartYear <= 0)
            {
                errorMessage.Add("Year Paid cannot be 0.");
            }
            if (MonthsPaidFor <= 0)
            {
                errorMessage.Add("Months Paid For cannot be 0.");
            }


            if (NextDueYear <= 0)
            {
                errorMessage.Add("Next Due Year cannot be 0.");
            }

            if (string.IsNullOrEmpty(MethodOfPayment))
            {
                errorMessage.Add("Method of Payment is Required.");
            }
            if (string.IsNullOrEmpty(TenantPaymentBank))
            {
                errorMessage.Add("Tenant Payment Bank is Required.");
            }


            if (string.IsNullOrEmpty(NextDueMonth))
            {
                errorMessage.Add("Next Due Month is Required.");
            }


            // Method to set session string with null check
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
            #endregion



            #region SettingUpSession
            //0-10
            SetSessionString("TenantID1", TenantID);
            SetSessionString("PropertyID1", PropertyID);
            string AmountPaidString = AmountPaid.ToString();
            HttpContext.Session.SetString("AmountPaid1", AmountPaidString);

            SetSessionString("PaymentStartMonth1", PaymentStartMonth);
            SetSessionString("PaymentEndMonth1", PaymentEndMonth);
            HttpContext.Session.SetInt32("PaymentStartYear1", PaymentStartYear);

            HttpContext.Session.SetInt32("MonthsPaidFor1", MonthsPaidFor);
            SetSessionString("NextDueMonth1", NextDueMonth);
            HttpContext.Session.SetInt32("NextDueYear1", NextDueYear);
            string DateOfRecordString = NextDueDate.ToString("yyyy-MM-dd");
            HttpContext.Session.SetString("NextDueDate1", DateOfRecordString);
            DateOfRecordString = DateOfTenantsPayment.ToString("yyyy-MM-dd");
            HttpContext.Session.SetString("DateOfTenantsPayment1", DateOfRecordString);
            SetSessionString("MethodOfPayment1", MethodOfPayment);
            SetSessionString("TenantPaymentBank1", TenantPaymentBank);
            DateOfRecordString = DateOfRecord.ToString("yyyy-MM-dd");
            HttpContext.Session.SetString("DateOfRecord1", DateOfRecordString);

            #endregion



            //if ModelState is true
            if (ModelState.IsValid == true)
            {


                Payment myPayment = new()
                {
                    TenantID = TenantID,
                    PropertyID = PropertyID,
                    AmountPaid = AmountPaid,
                    PaymentStartMonth = PaymentStartMonth,
                    PaymentEndMonth = PaymentEndMonth,
                    PaymentStartYear = PaymentStartYear,
                    MonthsPaidFor = MonthsPaidFor,
                    NextDueMonth = NextDueMonth,
                    NextDueYear = NextDueYear,
                    NextDueDate = NextDueDate,
                    DateOfTenantsPayment = DateOfTenantsPayment,
                    MethodOfPayment = MethodOfPayment,
                    TenantPaymentBank = TenantPaymentBank
                };


                RentReminder aReminder = new()
                {
                    TenantID = TenantID,
                    PropertyID = PropertyID,
                    LastRentAmountPaid = AmountPaid,
                    DateOfTenantsPayment = DateOfTenantsPayment,
                    NextDueDate = NextDueDate,
                    NextDueMonth = NextDueMonth,
                    NextDueYear = NextDueYear           
                };

                bool Confirmation;
                CNMPMS PaymentRequestDirector = new CNMPMS();
                Confirmation = PaymentRequestDirector.AddPayment(myPayment);
                if (Confirmation == true)
                {

                   bool Confirmation2;
                   CNMPMS RentReminderRequestDirector= new CNMPMS();
                   Confirmation2 = RentReminderRequestDirector.AddReminder(aReminder);
                    if (Confirmation2 == true)
                    {
                        Message = "Payment successful added and Reminder created.";
                    }
                    else
                    {
                        Message = "Payment successful added but Reminder was not created";

                    }


                    string messageBody = "Hello " + FName + ",\n" +
                                        "\nYour payment of has been saved. Below are the details for your record:\n" +
                                        "\t\t. You paid:" + AmountPaid + "\n\nRegards";
                    string messageSubject = "Payment submitted.";

                    string mailConfirmation;
                    CNMPMS MailRequestManager = new CNMPMS();
                    mailConfirmation = MailRequestManager.PostEmail(Email, messageBody, messageSubject);

                }
                else
                {

                    Message = "Payment was not added";
                }
                //return Page();
            }
            else
            {
                //repopulate the text
                Message = "This was not saved.";
                RePopulate();
            }
           RePopulate();

        }

       
   
 
        private void RePopulate()
        {
            // Retrieving session values
            //0-10
            PropertyID = HttpContext.Session.GetString("PropertyID1") ?? string.Empty;

            TenantID= HttpContext.Session.GetString("TenantID1") ?? string.Empty;
            PropertyID = HttpContext.Session.GetString("PropertyID1") ?? string.Empty;
           
            string amountPaidString = HttpContext.Session.GetString("AmountPaid1");

            // Convert string back to decimal
            if (decimal.TryParse(amountPaidString, out decimal amountPaid))
            {
                AmountPaid = amountPaid;
            }

            PaymentStartMonth = HttpContext.Session.GetString("PaymentStartMonth1") ?? string.Empty;
            PaymentEndMonth = HttpContext.Session.GetString("PaymentEndMonth1") ?? string.Empty;
            PaymentStartYear = HttpContext.Session.GetInt32("PaymentStartYear1") ?? DateTime.Now.Year;
            MonthsPaidFor = HttpContext.Session.GetInt32("MonthsPaidFor1") ?? 12;


            NextDueMonth = HttpContext.Session.GetString("NextDueMonth1") ?? string.Empty;
            NextDueYear  = HttpContext.Session.GetInt32("NextDueYear1") ?? DateTime.Now.Year;
            NextDueDate = DateTime.TryParse(HttpContext.Session.GetString("NextDueDate1"), out DateTime nextDueDate) ? nextDueDate : DateTime.MinValue;  
            DateOfTenantsPayment = DateTime.TryParse(HttpContext.Session.GetString("DateOfTenantsPayment1"), out DateTime dateOfTenantsPayment) ? dateOfTenantsPayment  : DateTime.MinValue; // Assuming you want to store DOB as a string


            MethodOfPayment  = HttpContext.Session.GetString("MethodOfPayment1") ?? string.Empty;
            TenantPaymentBank  = HttpContext.Session.GetString("TenantPaymentBank1") ?? string.Empty;


            TenantID = HttpContext.Session.GetString("TenantID1") ?? string.Empty;
            TenantID = HttpContext.Session.GetString("TenantID1") ?? string.Empty;
            DateOfRecord = DateTime.TryParse(HttpContext.Session.GetString("DateOfRecord1"), out DateTime dateOfRecord) ? dateOfRecord : DateTime.MinValue;  
        }


    }
 
}
