
using Domain.Contracts;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        // Use to retrieve Product by Id
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        // Use to Get All Products 
        public ProductWithBrandAndTypeSpecifications(string? sort, int? brandId, int? typeId)
            : base(product => 
                    (!brandId.HasValue || product.BrandId == brandId.Value) && 
                    (!typeId.HasValue || product.TypeId == typeId.Value))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch(sort.ToLower().Trim())
                {
                    case "pricedesc":
                        SetOrderByDescending(p => p.Price);
                        break;
                    case "priceasc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "namedesc":
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
        }
    }
}
