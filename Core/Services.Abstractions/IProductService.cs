﻿
namespace Services.Abstractions
{
    public interface IProductService
    {
        // Get All Products
        public Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
        // Get All Brands
        public Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        // Get All Types
        public Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();
        // Get Product By Id
        public Task<ProductResultDTO> GetProductByIdAsync(int id);
    }
}
