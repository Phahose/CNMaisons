namespace CNMaisons.Domain
{
    public class Token
    {
        public int TokenID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string TokenCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set;}
        public bool IsUsed { get; set; }       
    }
}
