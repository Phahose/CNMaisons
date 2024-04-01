namespace CNMaisons.Domain
{
    public class Property
    {
        public string PropertyID { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public string PropertyLocationState {  get; set; } = string.Empty;
        public string PropertyLocationCountry {  get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public int NumberOfRooms { get; set; }
        public decimal PropertyPrice { get; set; }
        public string PropertyDescription {  get; set; } = string.Empty;
        public byte[] Image1 { get; set; } = new byte[0];
        public byte[] Image2 { get; set; } = new byte[0];
        public byte[] Image3 { get; set; } = new byte[0];
        public byte[] Image4 { get; set; } = new byte[0];
        public byte[] Image5 { get; set; } = new byte[0];
        public byte[] Image6 { get; set; } = new byte[0];
        public byte[] Image7 { get; set; } = new byte[0];
        public byte[] Image8 { get; set; } = new byte[0];
        public byte[] Image9 { get; set; } = new byte[0];
        public byte[] Image10 { get; set; } = new byte[0];
        public int DeleteFlag {  get; set; } = 0;   
        public bool Occupied { get; set; }
    }
}
