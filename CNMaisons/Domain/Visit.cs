namespace CNMaisons.Domain
{
    public class Visit
    {

        public int PropertyVisitID { get; set; }
        public string PropertyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateRecorded { get; set; }
        public DateTime ProposedVisitDate { get; set; }
        public TimeSpan ProposedVisitTime { get; set; }
        public string VisitStatus { get; set; }
 
    }
}
