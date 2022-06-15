namespace Business.Layer.Entegration.Trendyol.GetShipmenPackages
{
    public class ShipmentAddress
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string company { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public int cityCode { get; set; }
        public string district { get; set; }
        public int districtId { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public int neighborhoodId { get; set; }
        public string neighborhood { get; set; }
        public object phone { get; set; }
        public string fullAddress { get; set; }
        public string fullName { get; set; }
    }
}
