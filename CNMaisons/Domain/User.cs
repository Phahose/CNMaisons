namespace CNMaisons.Domain
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;   
        public int AccountStatus { get; set; }
        public string UserSalt { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; } 
    }
}
