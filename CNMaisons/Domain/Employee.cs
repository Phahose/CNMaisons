namespace CNMaisons.Domain
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;   
        public int AccountStatus { get; set; }
        public string UserSalt { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; } 
    }
}
