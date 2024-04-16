namespace CNMaisons.Domain
{
    public class Reminder
    {
        public int ReminderId { get; set; }
        public string TenantID { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
        public DateTime DueDateRemindedFor { get; set; }

    }
}
