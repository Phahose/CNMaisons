namespace CNMaisons.Domain
{
    public class RentReminder
    {
        public int ReminderID { get; set; }
        public string TenantID { get; set; }
        public string PropertyID { get; set; }
        public decimal LastRentAmountPaid { get; set; }
        public DateTime DateOfTenantsPayment { get; set; }
        public DateTime NextDueDate { get; set; }
        public string NextDueMonth { get; set; }
        public int NextDueYear { get; set; }
    }
}
