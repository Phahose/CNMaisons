using CNMaisons.Controller;
using CNMaisons.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace CNMaisons.Pages
{
    public class LeaseApplicationModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public string errorMessage { get; set; } = string.Empty;

        [BindProperty]
        public IFormFile Passport { get; set; }

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
        public byte[] Signature { get; set; } = new byte[0];


        [BindProperty]
        public bool DeleteFlag { get; set; }


        [BindProperty]
        public string ApprovalStatus { get; set; } = string.Empty;

        public void OnGet()
        {
        }



        public IActionResult OnPost(IFormFile ApplicationForm)
        {
            errorMessage = "";



            //if (Passport == null)
            //{
            //    errorMessage += "The Passport is Required\n. ";
            //}

            if (string.IsNullOrEmpty(FirstName))
            {
                errorMessage += "The FirstName is Required\n. ";
            }

            if (string.IsNullOrEmpty(LastName))
            {
                errorMessage += "The LastName is Required\n. ";
            }

            // Validate PhoneNumber
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                errorMessage += "The PhoneNumber is Required\n. ";
            }

            // Validate Email
            if (string.IsNullOrEmpty(Email))
            {
                errorMessage += "The Email is Required\n. ";
            }

            // Validate DOB
            if (DOB == DateTime.MinValue)
            {
                errorMessage += "The DOB is Required\n. ";
            }

            // Validate Nationality
            if (string.IsNullOrEmpty(Nationality))
            {
                errorMessage += "The Nationality is Required\n. ";
            }
            // Validate PermanentHomeAddress
            if (string.IsNullOrEmpty(PermanentHomeAddress))
            {
                errorMessage += "The PermanentHomeAddress is Required\n. ";
            }

            // Validate Occupation
            if (string.IsNullOrEmpty(Occupation))
            {
                errorMessage += "The Occupation is Required\n. ";
            }

            // Validate SelfEmployed
            if (string.IsNullOrEmpty(SelfEmployed))
            {
                errorMessage += "The SelfEmployed is Required\n. ";
            }


            // Validate MaritalStatus
            if (string.IsNullOrEmpty(MaritalStatus))
            {
                errorMessage += "The MaritalStatus is Required\n. ";
            }

            // Validate SpouseFirstName
            // Validate NumberOfOccupants
            if (NumberOfOccupants == 0)
            {
                errorMessage += "The NumberOfOccupants is Required\n. ";
            }

            // Validate NextOfKinFirstName
            if (string.IsNullOrEmpty(NextOfKinFirstName))
            {
                errorMessage += "The NextOfKinFirstName is Required\n. ";
            }

            // Validate NextOfKinLastName
            if (string.IsNullOrEmpty(NextOfKinLastName))
            {
                errorMessage += "The NextOfKinLastName is Required\n. ";
            }

            // Validate NextOfKinAddress
            if (string.IsNullOrEmpty(NextOfKinAddress))
            {
                errorMessage += "The NextOfKinAddress is Required\n. ";
            }

            // Validate NextOfKinPhoneNumber
            if (string.IsNullOrEmpty(NextOfKinPhoneNumber))
            {
                errorMessage += "The NextOfKinPhoneNumber is Required\n. ";
            }

            // Validate Guarantor1FirstName
            if (string.IsNullOrEmpty(Guarantor1FirstName))
            {
                errorMessage += "The Guarantor1FirstName is Required\n. ";
            }

            // Validate Guarantor1LastName
            if (string.IsNullOrEmpty(Guarantor1LastName))
            {
                errorMessage += "The Guarantor1LastName is Required\n. ";
            }

            //// Validate Guarantor1Address
            //if (string.IsNullOrEmpty(Guarantor1Address))
            //{
            //    errorMessage += "The Guarantor1Address is Required\n. ";
            //}

            // Validate Guarantor1Occupation
            if (string.IsNullOrEmpty(Guarantor1Occupation))
            {
                errorMessage += "The Guarantor1Occupation is Required\n. ";
            }

            // Validate Guarantor1PhoneNumber
            if (string.IsNullOrEmpty(Guarantor1PhoneNumber))
            {
                errorMessage += "The Guarantor1PhoneNumber is Required\n. ";
            }


            // Validate Guarantor2FirstName
            if (string.IsNullOrEmpty(Guarantor2FirstName))
            {
                errorMessage += "The Guarantor2FirstName is Required\n. ";
            }

            // Validate Guarantor2LastName
            if (string.IsNullOrEmpty(Guarantor2LastName))
            {
                errorMessage += "The Guarantor2LastName is Required\n. ";
            }

            //// Validate Guarantor2Address
            //if (string.IsNullOrEmpty(Guarantor2Address))
            //{
            //    errorMessage += "The Guarantor2Address is Required\n. ";
            //}

            // Validate Guarantor2Occupation
            if (string.IsNullOrEmpty(Guarantor2Occupation))
            {
                errorMessage += "The Guarantor2Occupation is Required\n. ";
            }

            // Validate Guarantor2PhoneNumber
            if (string.IsNullOrEmpty(Guarantor2PhoneNumber))
            {
                errorMessage += "The Guarantor2PhoneNumber is Required\n. ";
            }


            // Validate Declaration
            if (string.IsNullOrEmpty(Declaration))
            {
                errorMessage += "The Declaration is Required\n. ";
            }


            // Check if any error occurred and add it to the model state
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("AllError", errorMessage);
            }

            if (!ModelState.IsValid)
            {
                // If ModelState is invalid, repopulate the model properties with the POSTed values
                // This will retain the form values when the page is rendered again
                Passport = Request.Form.Files["Passport"];
                FirstName = Request.Form["FirstName"];
                LastName = Request.Form["LastName"];
                PhoneNumber = Request.Form["PhoneNumber"];
                Email = Request.Form["Email"];

                string dobString = Request.Form["DOB"];
                if (DateTime.TryParseExact(dobString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                {
                    DOB = dob;
                }

                Nationality = Request.Form["Nationality"];
                StateofOrigin = Request.Form["StateofOrigin"];
                LGA = Request.Form["LGA"];
                HomeTown = Request.Form["HomeTown"];
                PermanentHomeAddress = Request.Form["PermanentHomeAddress"];
                Occupation = Request.Form["Occupation"];
                SelfEmployed = Request.Form["SelfEmployed"];
                BusinessRegistrationNumber = Request.Form["BusinessRegistrationNumber"];
                NameofEmployer = Request.Form["NameofEmployer"];
                AddressOfEmployer = Request.Form["AddressOfEmployer"];


                if (int.TryParse(Request.Form["LengthOnJob"], out int lengthOnJob))
                {
                    LengthOnJob = lengthOnJob;
                }

                CurrentPositionHeld = Request.Form["CurrentPositionHeld"];
                NatureOfJob = Request.Form["NatureOfJob"];
                FormerResidenceAddress = Request.Form["FormerResidenceAddress"];
                ReasonForMoving = Request.Form["ReasonForMoving"];

                if (int.TryParse(Request.Form["LengthOnJob"], out int lengthOfStayAtOldResidence))
                {
                    LengthOfStayAtOldResidence = lengthOfStayAtOldResidence;
                }

                NameOfFormerResidentManager = Request.Form["NameOfFormerResidentManager"];
                ObjectionsToReasonsForMoving = Request.Form["ObjectionsToReasonsForMoving"];
                MaritalStatus = Request.Form["MaritalStatus"];
                SpouseFirstName = Request.Form["SpouseFirstName"];
                SpouseLastName = Request.Form["SpouseLastName"];
                SpouseOccupation = Request.Form["SpouseOccupation"];

                if (int.TryParse(Request.Form["NumberOfOccupants"], out int numberOfOccupants))
                {
                    NumberOfOccupants = numberOfOccupants;
                }



                NextOfKinFirstName = Request.Form["NextOfKinFirstName"];
                NextOfKinLastName = Request.Form["NextOfKinLastName"];
                NextOfKinAddress = Request.Form["NextOfKinAddress"];
                NextOfKinPhoneNumber = Request.Form["NextOfKinPhoneNumber"];
                Guarantor1FirstName = Request.Form["Guarantor1FirstName"];
                Guarantor1LastName = Request.Form["Guarantor1LastName"];
                Guarantor1Address = Request.Form["Guarantor1Address"];
                Guarantor1Occupation = Request.Form["Guarantor1Occupation"];
                Guarantor1PhoneNumber = Request.Form["Guarantor1PhoneNumber"];
                Guarantor1AlternatePhoneNumber = Request.Form["Guarantor1AlternatePhoneNumber"];
                Guarantor2FirstName = Request.Form["Guarantor2FirstName"];
                Guarantor2LastName = Request.Form["Guarantor2LastName"];
                Guarantor2Address = Request.Form["Guarantor2Address"];
                Guarantor2Occupation = Request.Form["Guarantor2Occupation"];
                Guarantor2PhoneNumber = Request.Form["Guarantor2PhoneNumber"];
                Guarantor2AlternatePhoneNumber = Request.Form["Guarantor2AlternatePhoneNumber"];
                Declaration = Request.Form["Declaration"];
                ApprovalStatus = Request.Form["ApprovalStatus"];
                // Convert signature from base64 string
                string signatureBase64 = Request.Form["Signature"];
                if (!string.IsNullOrEmpty(signatureBase64))
                {
                    try
                    {
                        Signature = Convert.FromBase64String(signatureBase64);
                    }
                    catch (FormatException ex)
                    {
                        // Handle the case when the base64 string is not valid
                        //errorMessage += "Invalid signature format.\n";
                    }
                }

                // Convert DeleteFlag to boolean
                string deleteFlagString = Request.Form["DeleteFlag"];
                if (!string.IsNullOrEmpty(deleteFlagString))
                {
                    if (!bool.TryParse(deleteFlagString, out bool deleteFlag))
                    {
                        // Handle the case when the DeleteFlag is not a valid boolean
                        //errorMessage += "Invalid DeleteFlag value.\n";
                    }
                    else
                    {
                        DeleteFlag = deleteFlag;
                    }
                }

                // Return to the page without further processing
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Tenant aTenant;
                byte[] PassportBytes = ConvertToByteArray(Passport);

                aTenant = new()
                {
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
                    Signature = Signature,
                    ApprovalStatus = ApprovalStatus,
                    DeleteFlag = DeleteFlag
                };

                BCS tenantController = new();
                bool Confirmation = tenantController.SubmitLeaseApplication(aTenant);
                if (Confirmation == true)
                {
                    Message = "Reservation booking was successful.";
                }

            }
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

    }

}
