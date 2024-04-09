using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace CNMaisons.Pages
{
    public class LeaseApplicationModel : PageModel
    {
        #region AllDeclarations
        public string Message { get; set; } = string.Empty;
        public string errorMessage { get; set; } = string.Empty;

        [BindProperty]
        public IFormFile Passport { get; set; }
        

        [BindProperty]
        public string PropertyID { get; set; } 

        [BindProperty]
        public string FirstName { get; set; } 

        [BindProperty]
        public string LastName { get; set; } 

        [BindProperty]
        public string PhoneNumber { get; set; } 

        [BindProperty]
        public string Email { get; set; } 

        [BindProperty]
        public DateTime DOB { get; set; }

        [BindProperty]
        public string Password { get; set; } 

        [BindProperty]
        public string Nationality { get; set; } 

        [BindProperty]
        public string StateofOrigin { get; set; } 

        [BindProperty]
        public string LGA { get; set; } 

        [BindProperty]
        public string HomeTown { get; set; } 

        [BindProperty]
        public string PermanentHomeAddress { get; set; } 

        [BindProperty]
        public string Occupation { get; set; } 

        [BindProperty]
        public string SelfEmployed { get; set; } 

        [BindProperty]
        public string BusinessRegistrationNumber { get; set; } 



        [BindProperty]
        public IFormFile? CorporateAffairsCertificate { get; set; }




        [BindProperty]
        public string NameofEmployer { get; set; } 

        [BindProperty]
        public string AddressOfEmployer { get; set; } 

        [BindProperty]
        public int LengthOnJob { get; set; }

        [BindProperty]
        public string CurrentPositionHeld { get; set; } 

        [BindProperty]
        public string NatureOfJob { get; set; } 

        [BindProperty]
        public string FormerResidenceAddress { get; set; } 

        [BindProperty]
        public string ReasonForMoving { get; set; } 

        [BindProperty]
        public int LengthOfStayAtOldResidence { get; set; }

        [BindProperty]
        public string NameOfFormerResidentManager { get; set; } 

        [BindProperty]
        public string ObjectionsToReasonsForMoving { get; set; } 

        [BindProperty]
        public string MaritalStatus { get; set; } 

        [BindProperty]
        public string SpouseFirstName { get; set; } 

        [BindProperty]
        public string SpouseLastName { get; set; } 

        [BindProperty]
        public string SpouseOccupation { get; set; } 

        [BindProperty]
        public int NumberOfOccupants { get; set; }

        [BindProperty]
        public string NextOfKinFirstName { get; set; } 

        [BindProperty]
        public string NextOfKinLastName { get; set; } 

        [BindProperty]
        public string NextOfKinAddress { get; set; } 

        [BindProperty]
        public string NextOfKinPhoneNumber { get; set; } 

        [BindProperty]
        public string Guarantor1FirstName { get; set; } 

        [BindProperty]
        public string Guarantor1LastName { get; set; } 


        [BindProperty]
        public string Guarantor1Address { get; set; } 

        [BindProperty]
        public string Guarantor1Occupation { get; set; } 

        [BindProperty]
        public string Guarantor1PhoneNumber { get; set; } 

        [BindProperty]
        public string Guarantor1AlternatePhoneNumber { get; set; } 

        [BindProperty]
        public string Guarantor2FirstName { get; set; } 

        [BindProperty]
        public string Guarantor2LastName { get; set; } 

        [BindProperty]
        public string Guarantor2Address { get; set; } 

        [BindProperty]
        public string Guarantor2Occupation { get; set; } 

        [BindProperty]
        public string Guarantor2PhoneNumber { get; set; } 

        [BindProperty]
        public string Guarantor2AlternatePhoneNumber { get; set; } 

        [BindProperty]
        public string Declaration { get; set; }


        [BindProperty]
        public IFormFile? YourSignedForm { get; set; } 


        [BindProperty]
        public bool DeleteFlag { get; set; }


        [BindProperty]
        public string ApprovalStatus { get; set; } 


        [BindProperty]
        public IFormFile? LeaseFormForSigning { get; set; }
        #endregion


        public void OnGet()
        {
            
            //RePopulate();
        }

        CNMPMS PropertyRequestDirector = new CNMPMS();

        CNMPMS TenantRequestDirector = new CNMPMS();


        //public IActionResult OnPost(IFormFile ApplicationForm)
        public void OnPost(IFormFile ApplicationForm)
        {


            //if (Passport == null)
            //{
            //    errorMessage += "Passport is Required.\n";
            //}
            
            #region All Validations
            errorMessage = "";
            ModelState.Clear();

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




            // Validate Email
            if (string.IsNullOrEmpty(Email))
            {
                errorMessage += "Email is Required.\n";
            }
            else
            {
                bool emailConfirmation1 = TenantRequestDirector.ValidateEmailTenant(Email); // checking in Tenant Table
                bool emailConfirmation2 = TenantRequestDirector.ValidateEmailUsers(Email); // checking in Users table

                if (emailConfirmation1 == true || emailConfirmation2 == true)
                {
                    errorMessage += "This email already exist in our system.\n";
                } 
            }


            if (string.IsNullOrEmpty(FirstName))
            {
                errorMessage += "FirstName is Required.\n";
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errorMessage += "LastName is Required.\n";
            }

            // Validate PhoneNumber
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                errorMessage += "PhoneNumber is Required.\n";
            }



            
            // Validate DOB
            if (DOB == DateTime.MinValue)
            {
                errorMessage += "DOB is Required.\n";
            }

            // Validate Password
            if (string.IsNullOrEmpty(Password))
            {
                errorMessage += "Password is Required.\n";
            }

            // Validate Nationality
            if (string.IsNullOrEmpty(Nationality))
            {
                errorMessage += "Nationality is Required.\n";
            }
            // Validate PermanentHomeAddress
            if (string.IsNullOrEmpty(PermanentHomeAddress))
            {
                errorMessage += "PermanentHomeAddress is Required.\n";
            }

            // Validate Occupation
            if (string.IsNullOrEmpty(Occupation))
            {
                errorMessage += "Occupation is Required.\n";
            }

            // Validate SelfEmployed
            if (string.IsNullOrEmpty(SelfEmployed))
            {
                errorMessage += "SelfEmployed is Required.\n";
            }


            // Validate MaritalStatus
            if (string.IsNullOrEmpty(MaritalStatus))
            {
                errorMessage += "MaritalStatus is Required.\n";
            }

            // Validate SpouseFirstName
            // Validate NumberOfOccupants
            if (NumberOfOccupants == 0)
            {
                errorMessage += "NumberOfOccupants is Required.\n";
            }

            // Validate NextOfKinFirstName
            if (string.IsNullOrEmpty(NextOfKinFirstName))
            {
                errorMessage += "NextOfKinFirstName is Required.\n";
            }

            // Validate NextOfKinLastName
            if (string.IsNullOrEmpty(NextOfKinLastName))
            {
                errorMessage += "NextOfKinLastName is Required.\n";
            }

            // Validate NextOfKinAddress
            if (string.IsNullOrEmpty(NextOfKinAddress))
            {
                errorMessage += "NextOfKinAddress is Required.\n";
            }

            // Validate NextOfKinPhoneNumber
            if (string.IsNullOrEmpty(NextOfKinPhoneNumber))
            {
                errorMessage += "NextOfKinPhoneNumber is Required.\n";
            }

            // Validate Guarantor1FirstName
            if (string.IsNullOrEmpty(Guarantor1FirstName))
            {
                errorMessage += "Guarantor1FirstName is Required.\n";
            }

            // Validate Guarantor1LastName
            if (string.IsNullOrEmpty(Guarantor1LastName))
            {
                errorMessage += "Guarantor1LastName is Required.\n";
            }

            // Validate Guarantor1Address
            if (string.IsNullOrEmpty(Guarantor1Address))
            {
                errorMessage += "Guarantor1Address is Required.\n";
            }

            // Validate Guarantor1Occupation
            if (string.IsNullOrEmpty(Guarantor1Occupation))
            {
                errorMessage += "Guarantor1Occupation is Required.\n";
            }

            // Validate Guarantor1PhoneNumber
            if (string.IsNullOrEmpty(Guarantor1PhoneNumber))
            {
                errorMessage += "Guarantor1PhoneNumber is Required.\n";
            }


            // Validate Guarantor2FirstName
            if (string.IsNullOrEmpty(Guarantor2FirstName))
            {
                errorMessage += "Guarantor2FirstName is Required.\n";
            }

            // Validate Guarantor2LastName
            if (string.IsNullOrEmpty(Guarantor2LastName))
            {
                errorMessage += "Guarantor2LastName is Required.\n";
            }

            // Validate Guarantor2Address
            if (string.IsNullOrEmpty(Guarantor2Address))
            {
                errorMessage += "Guarantor2Address is Required.\n";
            }

            // Validate Guarantor2Occupation
            if (string.IsNullOrEmpty(Guarantor2Occupation))
            {
                errorMessage += "Guarantor2Occupation is Required.\n";
            }

            // Validate Guarantor2PhoneNumber
            if (string.IsNullOrEmpty(Guarantor2PhoneNumber))
            {
                errorMessage += "Guarantor2PhoneNumber is Required.\n";
            }



            // Validate Declaration
            if (string.IsNullOrEmpty(Declaration))
            {
                errorMessage += "Declaration is Required.\n";
            }


            // Check if any error occurred and add it to the model state
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("AllError", errorMessage);
            }

            User testexistinguser = new User();
            testexistinguser = TenantRequestDirector.GetUserByEmail(Email);

            if (testexistinguser.Password != "")
            {
                errorMessage += "This account Already Exists in the System Use a Diffrent Email\n";
            }

            // If ModelState is invalid, repopulate the model properties with the POSTed values
            // This will retain the form values when the page is rendered again

            //set initial value
            //1-10
            // Setting session for each property
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
            SetSessionString("PropertyID1", PropertyID);
            SetSessionString("FirstName1", FirstName);
            SetSessionString("LastName1", LastName);
            SetSessionString("PhoneNumber1", PhoneNumber);
            SetSessionString("Email1", Email);
            SetSessionString("DOB1", DOB.ToString()); // Assuming you want to store DOB as a string
            SetSessionString("Password1", Password);
            SetSessionString("Nationality1", Nationality);
            SetSessionString("StateofOrigin1", StateofOrigin);

            //11-20
            SetSessionString("LGA1", LGA);
            SetSessionString("HomeTown1", HomeTown);
            SetSessionString("PermanentHomeAddress1", PermanentHomeAddress);
            SetSessionString("Occupation1", Occupation);
            SetSessionString("SelfEmployed1", SelfEmployed);
            SetSessionString("BusinessRegistrationNumber1", BusinessRegistrationNumber);
            //HttpContext.Session.Set("CorporateAffairsCertificate1", CorporateAffairsCertificate);
            SetSessionString("NameofEmployer1", NameofEmployer);
            SetSessionString("AddressOfEmployer1", AddressOfEmployer);
            HttpContext.Session.SetInt32("LengthOnJob1", LengthOnJob); // Assuming LengthOnJob is an integer

            //21-30
            SetSessionString("CurrentPositionHeld1", CurrentPositionHeld);
            SetSessionString("NatureOfJob1", NatureOfJob);
            SetSessionString("FormerResidenceAddress1", FormerResidenceAddress);
            SetSessionString("ReasonForMoving1", ReasonForMoving);
            HttpContext.Session.SetInt32("LengthOfStayAtOldResidence1", LengthOfStayAtOldResidence); // Assuming LengthOfStayAtOldResidence is an integer
            SetSessionString("NameOfFormerResidentManager1", NameOfFormerResidentManager);
            SetSessionString("ObjectionsToReasonsForMoving1", ObjectionsToReasonsForMoving);
            SetSessionString("MaritalStatus1", MaritalStatus);
            SetSessionString("SpouseFirstName1", SpouseFirstName);
            SetSessionString("SpouseLastName1", SpouseLastName);

            //31-40
            SetSessionString("SpouseOccupation1", SpouseOccupation);
            HttpContext.Session.SetInt32("NumberOfOccupants1", NumberOfOccupants); // Assuming NumberOfOccupants is an integer
            SetSessionString("NextOfKinFirstName1", NextOfKinFirstName);
            SetSessionString("NextOfKinLastName1", NextOfKinLastName);
            SetSessionString("NextOfKinAddress1", NextOfKinAddress);
            SetSessionString("NextOfKinPhoneNumber1", NextOfKinPhoneNumber);
            SetSessionString("Guarantor1FirstName1", Guarantor1FirstName);
            SetSessionString("Guarantor1LastName1", Guarantor1LastName);
            SetSessionString("Guarantor1Address1", Guarantor1Address);
            SetSessionString("Guarantor1Occupation1", Guarantor1Occupation);

            //41-50
            SetSessionString("Guarantor1PhoneNumber1", Guarantor1PhoneNumber);
            SetSessionString("Guarantor1AlternatePhoneNumber1", Guarantor1AlternatePhoneNumber);
            SetSessionString("Guarantor2FirstName1", Guarantor2FirstName);
            SetSessionString("Guarantor2LastName1", Guarantor2LastName);
            SetSessionString("Guarantor2Address1", Guarantor2Address);
            SetSessionString("Guarantor2Occupation1", Guarantor2Occupation);
            SetSessionString("Guarantor2PhoneNumber1", Guarantor2PhoneNumber);
            SetSessionString("Guarantor2AlternatePhoneNumber1", Guarantor2AlternatePhoneNumber);
            SetSessionString("Declaration1", Declaration);
            
            //51-52

            SetSessionString("ApprovalStatus1", ApprovalStatus);
            
            SetSessionString("DeleteFlag1", DeleteFlag.ToString());
            #endregion



            //if ModelState is true
            if (ModelState.IsValid == true)
            {

                Tenant aTenant;
                byte[] PassportBytes = ConvertToByteArray(Passport);
                byte[] CorporateAffairs = ConvertToByteArray(CorporateAffairsCertificate);
                byte[] LeaseForm = ConvertToByteArray(LeaseFormForSigning);
                byte[] SignedForm = ConvertToByteArray(YourSignedForm);

                aTenant = new()
                {
                    PropertyID = PropertyID,
                    Passport = PassportBytes,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    DOB = DOB,
                    Nationality = Nationality,
                    StateofOrigin = StateofOrigin,
                    LGA = LGA,
                    HomeTown = HomeTown,
                    PermanentHomeAddress = PermanentHomeAddress,
                    Occupation = Occupation,
                    SelfEmployed = SelfEmployed,
                    BusinessRegistrationNumber = BusinessRegistrationNumber,
                    CorporateAffairsCertificate = CorporateAffairs,
                    NameofEmployer = NameofEmployer,
                    AddressOfEmployer = AddressOfEmployer,
                    LengthOnJob = LengthOnJob,
                    CurrentPositionHeld = CurrentPositionHeld,
                    NatureOfJob = NatureOfJob,
                    FormerResidenceAddress = FormerResidenceAddress,
                    ReasonForMoving = ReasonForMoving,
                    LengthOfStayAtOldResidence = LengthOfStayAtOldResidence,
                    NameOfFormerResidentManager = NameOfFormerResidentManager,
                    ObjectionsToReasonsForMoving = ObjectionsToReasonsForMoving,
                    MaritalStatus = MaritalStatus,
                    SpouseFirstName = SpouseFirstName,
                    SpouseLastName = SpouseLastName,
                    SpouseOccupation = SpouseOccupation,
                    NumberOfOccupants = NumberOfOccupants,
                    NextOfKinFirstName = NextOfKinFirstName,
                    NextOfKinLastName = NextOfKinLastName,
                    NextOfKinAddress = NextOfKinAddress,
                    NextOfKinPhoneNumber = NextOfKinPhoneNumber,
                    Guarantor1FirstName = Guarantor1FirstName,
                    Guarantor1LastName = Guarantor1LastName,
                    Guarantor1Address = Guarantor1Address,
                    Guarantor1Occupation = Guarantor1Occupation,
                    Guarantor1PhoneNumber = Guarantor1PhoneNumber,
                    Guarantor1AlternatePhoneNumber = Guarantor1AlternatePhoneNumber,
                    Guarantor2FirstName = Guarantor2FirstName,
                    Guarantor2LastName = Guarantor2LastName,
                    Guarantor2Address = Guarantor2Address,
                    Guarantor2Occupation = Guarantor2Occupation,
                    Guarantor2PhoneNumber = Guarantor2PhoneNumber,
                    Guarantor2AlternatePhoneNumber = Guarantor2AlternatePhoneNumber,
                    Declaration = Declaration,
                    YourSignedForm = SignedForm,
                    ApprovalStatus = "Just Applied",
                    DeleteFlag = DeleteFlag,
                    LeaseFormForSigning = LeaseForm
                };




                TenantRequestDirector = new();
                string Confirmation = TenantRequestDirector.SubmitLeaseApplication(aTenant);
                if (Confirmation == "Successful.")
                {
                    Message = "Tenant's Lease application was successful saved.";
                    User newUser = new();
                    newUser.Email = HttpContext.Session.GetString("Email1") ?? string.Empty;
                    newUser.Password = HttpContext.Session.GetString("Password1") ?? string.Empty;
                    newUser.UserSalt = "placeholder";
                    newUser.Role = "Tenant";
                    newUser.DefaultPassword = "0"; // 0 because user typed it by himself

                    bool UserAccountConfirmation = TenantRequestDirector.CreateUserAccount(newUser);
                    if (UserAccountConfirmation == true)
                    {
                        string messageBody = "Hello " + FirstName + ",\n\nYour Login Credential are:\n\t\temail: "+ Email + "\n\t\tPassword: "+ Password +"\n\n Lease Application for " + PropertyID + " has been submitted.\n\nOnce Attended to, you will get a feedback.\n\nRegards";
                        string messageSubject = "Application submitted.";

                        string mailConfirmation;
                        CNMPMS MailRequestManager = new CNMPMS();
                        mailConfirmation = MailRequestManager.PostEmail(Email, messageBody, messageSubject); 
                        
                        Message = "Tenant's Lease application was successful saved and Login Account created.";
                    }
                    else
                    {
                         
                        Message = "Tenant's Lease application was successful saved. Login WAS NOT created. Admin need to create one for him";
                    }
                    //return Page();
                }
                else
                {
                    //repopulate the text
                    Message = Confirmation;  // "This was not saved.";
                    RePopulate();
                    //return Page();
                }
                

                
            }

            RePopulate();
            //return Page();
        } 

        private byte[] ConvertToByteArray(IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            else { return null; }
        }

        private void RePopulate()
        {
            // Retrieving session values
            //0-10
            PropertyID = HttpContext.Session.GetString("PropertyID1") ?? string.Empty;
            //Passport = HttpContext.Session.GetString("Passport1") ?? string.Empty;
            FirstName = HttpContext.Session.GetString("FirstName1") ?? string.Empty;
            LastName = HttpContext.Session.GetString("LastName1") ?? string.Empty;
            PhoneNumber = HttpContext.Session.GetString("PhoneNumber1") ?? string.Empty;
            Email = HttpContext.Session.GetString("Email1") ?? string.Empty;
            DOB = DateTime.TryParse(HttpContext.Session.GetString("DOB1"), out DateTime dob) ? dob : DateTime.MinValue; // Assuming you want to store DOB as a string
            Password = HttpContext.Session.GetString("Password1") ?? string.Empty;
            Nationality = HttpContext.Session.GetString("Nationality1") ?? string.Empty;
            StateofOrigin = HttpContext.Session.GetString("StateofOrigin1") ?? string.Empty;

            //11-20
            LGA = HttpContext.Session.GetString("LGA1") ?? string.Empty;
            HomeTown = HttpContext.Session.GetString("HomeTown1") ?? string.Empty;
            PermanentHomeAddress = HttpContext.Session.GetString("PermanentHomeAddress1") ?? string.Empty;
            Occupation = HttpContext.Session.GetString("Occupation1") ?? string.Empty;
            SelfEmployed = HttpContext.Session.GetString("SelfEmployed1") ?? string.Empty;
            BusinessRegistrationNumber = HttpContext.Session.GetString("BusinessRegistrationNumber1") ?? string.Empty;
            //CorporateAffairsCertificate = HttpContext.Session.Get<byte[]>("CorporateAffairsCertificate1") ?? new byte[0];
            NameofEmployer = HttpContext.Session.GetString("NameofEmployer1") ?? string.Empty;
            AddressOfEmployer = HttpContext.Session.GetString("AddressOfEmployer1") ?? string.Empty;
            LengthOnJob = HttpContext.Session.GetInt32("LengthOnJob1") ?? 0; // Assuming LengthOnJob is an integer

            //21-30
            CurrentPositionHeld = HttpContext.Session.GetString("CurrentPositionHeld1") ?? string.Empty;
            NatureOfJob = HttpContext.Session.GetString("NatureOfJob1") ?? string.Empty;
            FormerResidenceAddress = HttpContext.Session.GetString("FormerResidenceAddress1") ?? string.Empty;
            ReasonForMoving = HttpContext.Session.GetString("ReasonForMoving1") ?? string.Empty;
            LengthOfStayAtOldResidence = HttpContext.Session.GetInt32("LengthOfStayAtOldResidence1") ?? 0; // Assuming LengthOfStayAtOldResidence is an integer
            NameOfFormerResidentManager = HttpContext.Session.GetString("NameOfFormerResidentManager1") ?? string.Empty;
            ObjectionsToReasonsForMoving = HttpContext.Session.GetString("ObjectionsToReasonsForMoving1") ?? string.Empty;
            MaritalStatus = HttpContext.Session.GetString("MaritalStatus1") ?? string.Empty;
            SpouseFirstName = HttpContext.Session.GetString("SpouseFirstName1") ?? string.Empty;
            SpouseLastName = HttpContext.Session.GetString("SpouseLastName1") ?? string.Empty;

            //31-40
            SpouseOccupation = HttpContext.Session.GetString("SpouseOccupation1") ?? string.Empty;
            NumberOfOccupants = HttpContext.Session.GetInt32("NumberOfOccupants1") ?? 0; // Assuming NumberOfOccupants is an integer
            NextOfKinFirstName = HttpContext.Session.GetString("NextOfKinFirstName1") ?? string.Empty;
            NextOfKinLastName = HttpContext.Session.GetString("NextOfKinLastName1") ?? string.Empty;
            NextOfKinAddress = HttpContext.Session.GetString("NextOfKinAddress1") ?? string.Empty;
            NextOfKinPhoneNumber = HttpContext.Session.GetString("NextOfKinPhoneNumber1") ?? string.Empty;
            Guarantor1FirstName = HttpContext.Session.GetString("Guarantor1FirstName1") ?? string.Empty;
            Guarantor1LastName = HttpContext.Session.GetString("Guarantor1LastName1") ?? string.Empty;
            Guarantor1Address = HttpContext.Session.GetString("Guarantor1Address1") ?? string.Empty;
            Guarantor1Occupation = HttpContext.Session.GetString("Guarantor1Occupation1") ?? string.Empty;

            //41-50
            Guarantor1PhoneNumber = HttpContext.Session.GetString("Guarantor1PhoneNumber1") ?? string.Empty;
            Guarantor1AlternatePhoneNumber = HttpContext.Session.GetString("Guarantor1AlternatePhoneNumber1") ?? string.Empty;
            Guarantor2FirstName = HttpContext.Session.GetString("Guarantor2FirstName1") ?? string.Empty;
            Guarantor2LastName = HttpContext.Session.GetString("Guarantor2LastName1") ?? string.Empty;
            Guarantor2Address = HttpContext.Session.GetString("Guarantor2Address1") ?? string.Empty;
            Guarantor2Occupation = HttpContext.Session.GetString("Guarantor2Occupation1") ?? string.Empty;
            Guarantor2PhoneNumber = HttpContext.Session.GetString("Guarantor2PhoneNumber1") ?? string.Empty;
            Guarantor2AlternatePhoneNumber = HttpContext.Session.GetString("Guarantor2AlternatePhoneNumber1") ?? string.Empty;
            Declaration = HttpContext.Session.GetString("Declaration1") ?? string.Empty;
             
            //51-52
            ApprovalStatus = HttpContext.Session.GetString("ApprovalStatus1") ?? string.Empty;
            DeleteFlag = Convert.ToBoolean(HttpContext.Session.GetString("DeleteFlag1") ?? "false");

        }


    }

}
