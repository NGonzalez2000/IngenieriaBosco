using IngenieriaBosco.Core.Models.Generics;

namespace IngenieriaBosco.Core.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public GridListModel<BrandModel>? Brands { get; set; }
    }
}
