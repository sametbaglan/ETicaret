using System.Collections.Generic;

namespace Business.Layer.Entegration.Trendyol.GetShipmenPackages
{
    public class ShipmenContent
    {
        public ShipmentAddress shipmentAddress { get; set; }
        public string orderNumber { get; set; }
        public double grossAmount { get; set; }
        public double totalDiscount { get; set; }
        public object taxNumber { get; set; }
        public InvoiceAddress invoiceAddress { get; set; }
        public string customerFirstName { get; set; }
        public string customerEmail { get; set; }
        public int customerId { get; set; }
        public string customerLastName { get; set; }
        public int id { get; set; }
        public object cargoTrackingNumber { get; set; }
        public string cargoTrackingLink { get; set; }
        public string cargoSenderNumber { get; set; }
        public string cargoProviderName { get; set; }
        public List<Line> lines { get; set; }
        public object orderDate { get; set; }
        public string tcIdentityNumber { get; set; }
        public string currencyCode { get; set; }
        public List<PackageHistory>  packages { get; set; }
        public string shipmentPackageStatus { get; set; }
        public string status { get; set; }
        public string deliveryType { get; set; }
        public int timeSlotId { get; set; }
        public string scheduledDeliveryStoreId { get; set; }
        public object estimatedDeliveryStartDate { get; set; }
        public object estimatedDeliveryEndDate { get; set; }
        public double totalPrice { get; set; }
        public string deliveryAddressType { get; set; }
        public object agreedDeliveryDate { get; set; }
        public bool fastDelivery { get; set; }
        public object originShipmentDate { get; set; }
        public object lastModifiedDate { get; set; }
    }
}
