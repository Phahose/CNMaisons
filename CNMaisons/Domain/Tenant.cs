using System;

namespace CNMaisons.Domain
{
    public class Tenant
    {
        public string TenantID { get; set; } = string.Empty;
        public string PropertyID { get; set; } = string.Empty;
        public byte[] Passport { get; set; } = new byte[0];
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string StateofOrigin { get; set; } = string.Empty;
        public string LGA { get; set; } = string.Empty;
        public string HomeTown { get; set; } = string.Empty;
        public string PermanentHomeAddress { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string SelfEmployed { get; set; } = string.Empty;
        public string BusinessRegistrationNumber { get; set; } = string.Empty;
        public byte[] CoporateAffairsCertificate { get; set; } = new byte[0];
        public string NameofEmployer { get; set; } = string.Empty;
        public string AddressOfEmployer { get; set; } = string.Empty;
        public int LengthOnJob { get; set; }
        public string CurrentPositionHeld { get; set; } = string.Empty;
        public string NatureOfJob { get; set; } = string.Empty;
        public string FormerResidenceAddress { get; set; } = string.Empty;
        public string ReasonForMoving { get; set; } = string.Empty;
        public int LengthOfStayAtOldResidence { get; set; }
        public string NameOfFormerResidentManager { get; set; } = string.Empty;
        public string ObjectionsToReasonsForMoving { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public string SpouseFirstName { get; set; } = string.Empty;
        public string SpouseLastName { get; set; } = string.Empty;
        public string SpouseOccupation { get; set; } = string.Empty;
        public int NumberOfOccupants { get; set; }
        public string NextOfKinFirstName { get; set; } = string.Empty;
        public string NextOfKinLastName { get; set; } = string.Empty;
        public string NextOfKinAddress { get; set; } = string.Empty;
        public string NextOfKinPhoneNumber { get; set; } = string.Empty;
        public string Guarantor1FirstName { get; set; } = string.Empty;
        public string Guarantor1LastName { get; set; } = string.Empty;
        public string Guarantor1Address { get; set; } = string.Empty;
        public string Guarantor1Occupation { get; set; } = string.Empty;
        public string Guarantor1PhoneNumber { get; set; } = string.Empty;
        public string Guarantor1AlternatePhoneNumber { get; set; } = string.Empty;
        public string Guarantor2FirstName { get; set; } = string.Empty;
        public string Guarantor2LastName { get; set; } = string.Empty;
        public string Guarantor2Address { get; set; } = string.Empty;
        public string Guarantor2Occupation { get; set; } = string.Empty;
        public string Guarantor2PhoneNumber { get; set; } = string.Empty;
        public string Guarantor2AlternatePhoneNumber { get; set; } = string.Empty;
        public string Declaration { get; set; } = string.Empty;
        public string YourSignature { get; set; } = string.Empty;
        public string ApprovalStatus { get; set; } = string.Empty;
        public bool DeleteFlag { get; set; }


    }
}
