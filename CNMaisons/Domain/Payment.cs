namespace CNMaisons.Domain
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string TenantID { get; set; }
        public string PropertyID { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentStartMonth { get; set; }
        public string PaymentEndMonth { get; set; }
        public int PaymentStartYear { get; set; }
        public int MonthsPaidFor { get; set; }
        public string NextDueMonth { get; set; }
        public int NextDueYear { get; set; }
        public DateTime NextDueDate { get; set; }
        public DateTime DateOfTenantsPayment { get; set; }
        public string MethodOfPayment { get; set; }
        public string TenantPaymentBank { get; set; }
        public DateTime DateOfRecord { get; set; }
    }

}
