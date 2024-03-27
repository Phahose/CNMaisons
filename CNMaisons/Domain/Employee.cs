namespace CNMaisons.Domain
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; }
        public byte[] EmployeeImage { get; set; } = new byte[0];
    }
}
