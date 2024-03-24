namespace CNMaisons.Domain
{
    public class User
    {
        public string FIrstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string DeactivateAccountStatus { get; set; } = string.Empty;
        public string DefaultPassword { get; set; } = string.Empty;
        public string UserSalt { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
    }
}
