using System.Collections.Generic;

namespace Business.Layer.Entegration.Trendyol.ProductAddPost
{
    public class TrendyolProduct
    {
        public string barcode { get; set; }
        public string title { get; set; }
        public string productMainId { get; set; }
        public int brandId { get; set; }
        public int categoryId { get; set; }
        public int quantity { get; set; }
        public string stockCode { get; set; }
        public int dimensionalWeight { get; set; }
        public string description { get; set; }
        public string currencyType { get; set; }
        public double listPrice { get; set; }
        public double salePrice { get; set; }
        public int vatRate { get; set; }
        public int cargoCompanyId { get; set; }
        public List<Image> images { get; set; }
        public List<Attribute> attributes { get; set; }
    }
}
