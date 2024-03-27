using Microsoft.AspNetCore.Mvc;


namespace CNMaisons.Domain
{
    public class Maintenance
    {
        public string PropertyID { get; set; } = string.Empty;
        public string TenantID { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string MaintenanceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public byte[] Image1 { get; set; } = new byte[0];
        public byte[] Image2 { get; set; } = new byte[0];
        public string Remarks { get; set; } = string.Empty;
        public DateOnly CompletionDate { get; set; }
        public DateOnly ApprovedStartDate { get; set; }
        public decimal ActualCost { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
