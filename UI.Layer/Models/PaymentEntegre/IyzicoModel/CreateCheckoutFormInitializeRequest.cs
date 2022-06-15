using System.Collections.Generic;

namespace UI.Layer.Models.PaymentEntegre.IyzicoModel
{
    public class CreateCheckoutFormInitializeRequest
    {
        public string locale { get; set; }
        public string conversationıd { get; set; }
        public decimal price { get; set; }
        public decimal paidPrice { get; set; }
        public string currency { get; set; }
        public string basketId { get; set; }
        public string paymentChannel { get; set; }
        public string paymentGroup { get; set; }
        public string callbackUrl { get; set; }
        public List<int> EnabledInstallments { get; set; }
   
        public Shipping Shipping { get; set; }
        public Billing Billing { get; set; }
        public List<Basket> BasketItems { get; set; }


    }
}
