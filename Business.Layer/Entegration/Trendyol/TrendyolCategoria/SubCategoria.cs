using System.Collections.Generic;

namespace Business.Layer.Entegration.Trendyol.TrendyolCategoria
{
    public class SubCategoria
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentId { get; set; }
        public List<TrendyolCategoria> subCategories { get; set; }
    }
}
