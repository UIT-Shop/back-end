﻿namespace MyShop.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        Task<ServiceResponse<List<ProductType>>> GetProductTypes();
        Task<ServiceResponse<List<ProductType>>> AddProductType(ProductType productType);
        Task<ServiceResponse<List<ProductType>>> UpdateProductType(ProductType productType);
        Task<ServiceResponse<bool>> DeleteProductType(int productTypeId);

    }
}