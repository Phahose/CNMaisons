using CNMaisons.Controller;
using CNMaisons.Domain;
using CNMaisons.TechnicalService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;

namespace CNMaisons.Pages
{
    public class LeaseApplicationModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
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
            //RePopulate();
        }



        public IActionResult OnPost(IFormFile ApplicationForm)
        {
            errorMessage = "";
            ModelState.Clear();

            //if (Passport == null)
            //{
            //    errorMessage += "The Passport is Required.\n";
            //}

            if (string.IsNullOrEmpty(TenantID))
            {
                errorMessage += "The TenantID is Required.\n";
            }

            if (string.IsNullOrEmpty(PropertyID))
            {
                errorMessage += "The PropertyID is Required.\n";
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                errorMessage += "The FirstName is Required.\n";
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errorMessage += "The LastName is Required.\n";
            }

            // Validate PhoneNumber
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                errorMessage += "The PhoneNumber is Required.\n";
            }

            // Validate Email
            if (string.IsNullOrEmpty(Email))
            {
                errorMessage += "The Email is Required.\n";
            }

            // Validate DOB
            if (DOB == DateTime.MinValue)
            {
                errorMessage += "The DOB is Required.\n";
            }

            // Validate Password
            if (string.IsNullOrEmpty(Password))
            {
                errorMessage += "The Password is Required.\n";
            }

            // Validate Nationality
            if (string.IsNullOrEmpty(Nationality))
            {
                errorMessage += "The Nationality is Required.\n";
            }
            // Validate PermanentHomeAddress
            if (string.IsNullOrEmpty(PermanentHomeAddress))
            {
                errorMessage += "The PermanentHomeAddress is Required.\n";
            }

            // Validate Occupation
            if (string.IsNullOrEmpty(Occupation))
            {
                errorMessage += "The Occupation is Required.\n";
            }

            // Validate SelfEmployed
            if (string.IsNullOrEmpty(SelfEmployed))
            {
                errorMessage += "The SelfEmployed is Required.\n";
            }


            // Validate MaritalStatus
            if (string.IsNullOrEmpty(MaritalStatus))
            {
                errorMessage += "The MaritalStatus is Required.\n";
            }

            // Validate SpouseFirstName
            // Validate NumberOfOccupants
            if (NumberOfOccupants == 0)
            {
                errorMessage += "The NumberOfOccupants is Required.\n";
            }

            // Validate NextOfKinFirstName
            if (string.IsNullOrEmpty(NextOfKinFirstName))
            {
                errorMessage += "The NextOfKinFirstName is Required.\n";
            }

            // Validate NextOfKinLastName
            if (string.IsNullOrEmpty(NextOfKinLastName))
            {
                errorMessage += "The NextOfKinLastName is Required.\n";
            }

            // Validate NextOfKinAddress
            if (string.IsNullOrEmpty(NextOfKinAddress))
            {
                errorMessage += "The NextOfKinAddress is Required.\n";
            }

            // Validate NextOfKinPhoneNumber
            if (string.IsNullOrEmpty(NextOfKinPhoneNumber))
            {
                errorMessage += "The NextOfKinPhoneNumber is Required.\n";
            }

            // Validate Guarantor1FirstName
            if (string.IsNullOrEmpty(Guarantor1FirstName))
            {
                errorMessage += "The Guarantor1FirstName is Required.\n";
            }

            // Validate Guarantor1LastName
            if (string.IsNullOrEmpty(Guarantor1LastName))
            {
                errorMessage += "The Guarantor1LastName is Required.\n";
            }

            // Validate Guarantor1Address
            if (string.IsNullOrEmpty(Guarantor1Address))
            {
                errorMessage += "The Guarantor1Address is Required.\n";
            }

            // Validate Guarantor1Occupation
            if (string.IsNullOrEmpty(Guarantor1Occupation))
            {
                errorMessage += "The Guarantor1Occupation is Required.\n";
            }

            // Validate Guarantor1PhoneNumber
            if (string.IsNullOrEmpty(Guarantor1PhoneNumber))
            {
                errorMessage += "The Guarantor1PhoneNumber is Required.\n";
            }


            // Validate Guarantor2FirstName
            if (string.IsNullOrEmpty(Guarantor2FirstName))
            {
                errorMessage += "The Guarantor2FirstName is Required.\n";
            }

            // Validate Guarantor2LastName
            if (string.IsNullOrEmpty(Guarantor2LastName))
            {
                errorMessage += "The Guarantor2LastName is Required.\n";
            }

            // Validate Guarantor2Address
            if (string.IsNullOrEmpty(Guarantor2Address))
            {
                errorMessage += "The Guarantor2Address is Required.\n";
            }

            // Validate Guarantor2Occupation
            if (string.IsNullOrEmpty(Guarantor2Occupation))
            {
                errorMessage += "The Guarantor2Occupation is Required.\n";
            }

            // Validate Guarantor2PhoneNumber
            if (string.IsNullOrEmpty(Guarantor2PhoneNumber))
            {
                errorMessage += "The Guarantor2PhoneNumber is Required.\n";
            }


            // Validate YourSignature
            if (string.IsNullOrEmpty(YourSignature))
            {
                errorMessage += "Your full name is required as signature.\n";
            }

            // Validate Declaration
            if (string.IsNullOrEmpty(Declaration))
            {
                errorMessage += "The Declaration is Required.\n";
            }


            // Check if any error occurred and add it to the model state
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("AllError", errorMessage);
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


            //0-10
            SetSessionString("TenantID1", TenantID);
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
            //HttpContext.Session.Set("CoporateAffairsCertificate1", CoporateAffairsCertificate);
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
            SetSessionString("YourSignature1", YourSignature);

            //51-52

            SetSessionString("ApprovalStatus1", ApprovalStatus);
            SetSessionString("DeleteFlag1", DeleteFlag.ToString());

            //if ModelState is true
            if (ModelState.IsValid == true)
            {

                Tenant aTenant;
                byte[] PassportBytes = ConvertToByteArray(Passport);

                aTenant = new()
                {
                    TenantID = TenantID,
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
                    CoporateAffairsCertificate = CoporateAffairsCertificate,
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
                    YourSignature = YourSignature,
                    ApprovalStatus = ApprovalStatus,
                    DeleteFlag = DeleteFlag

                };

                CNMPMS RequestDirector = new();
                string Confirmation = RequestDirector.SubmitLeaseApplication(aTenant);
                if (Confirmation == "Successful!")
                {
                    Message = "Tenant's Lease application was successful saved.";
                    User newUser = new();
                    newUser.Email = HttpContext.Session.GetString("Email1") ?? string.Empty;
                    newUser.Password = HttpContext.Session.GetString("Password1") ?? string.Empty;
                    newUser.UserSalt = "placeholder";
                    newUser.Role = "Tenant";
                    newUser.DefaultPassword = "0"; // 0 because user typed it by himself

                    bool UserAccountConfirmation = RequestDirector.CreateUserAccount(newUser);
                    if (UserAccountConfirmation == true)
                    {
                        Message = "Tenant's Lease application was successful saved and Login Account created.";
                    }
                    else
                    {
                        Message = "Tenant's Lease application was successful saved. Login WAS NOT created. Admin need to create one for him";
                    }




                    return Page();
                }
                else
                {
                    //repopulate the text
                    Message = Confirmation;
                    RePopulate();
                    return Page();
                }
            }
            RePopulate();
            return Page();
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
            TenantID = HttpContext.Session.GetString("TenantID1") ?? string.Empty;
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
            //CoporateAffairsCertificate = HttpContext.Session.Get<byte[]>("CoporateAffairsCertificate1") ?? new byte[0];
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
            YourSignature = HttpContext.Session.GetString("YourSignature1") ?? string.Empty;

            //51-52
            ApprovalStatus = HttpContext.Session.GetString("ApprovalStatus1") ?? string.Empty;
            DeleteFlag = Convert.ToBoolean(HttpContext.Session.GetString("DeleteFlag1") ?? "false");

        }

    }

}
