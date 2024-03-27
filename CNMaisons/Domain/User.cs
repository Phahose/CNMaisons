namespace CNMaisons.Domain
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool DeactivateAccountStatus { get; set; }
        public string DefaultPassword { get; set; } = string.Empty;
        public string UserSalt { get; set; } = string.Empty;
    }
}
