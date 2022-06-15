using System.Collections.Generic;

namespace Business.Layer.Entegration.Trendyol.GetShipmenPackages
{
    public class ShipmenRoot
    {
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public List<ShipmenContent> content { get; set; }
    }
}
