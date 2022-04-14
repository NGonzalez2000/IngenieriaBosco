using IngenieriaBosco.Core.Models.Generics;

namespace IngenieriaBosco.Core.Models
{
    public class BrandModel
    {
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsUSValue { get; set; }
        public GridListModel<ProviderModel>? Providers { get; set; }
    }
}
