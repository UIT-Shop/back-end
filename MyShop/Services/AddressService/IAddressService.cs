namespace MyShop.Services.AddressService
{
    public interface IAddressService
    {
        Task<ServiceResponse<Address>> GetAddress();
        Task<ServiceResponse<List<Province>>> GetProvinces();
        Task<ServiceResponse<List<District>>> GetDistricts(int provinceId);
        Task<ServiceResponse<List<Ward>>> GetWards(int districtId, int provinceId);

        Task<ServiceResponse<Address>> AddAddress(Address address);
    }
}
