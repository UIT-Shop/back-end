using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Address>>> GetAddress()
        {
            return await _addressService.GetAddress();
        }

        [HttpGet("provinces")]
        public async Task<ActionResult<ServiceResponse<List<Province>>>> GetProvinces()
        {
            return await _addressService.GetProvinces();
        }

        [HttpGet("provinces/{provinceId}/districts")]
        public async Task<ActionResult<ServiceResponse<List<District>>>> GetDistricts(int provinceId)
        {
            return await _addressService.GetDistricts(provinceId);
        }

        [HttpGet("provinces/{provinceId}/districts/{districtId}/wards")]
        public async Task<ActionResult<ServiceResponse<List<Ward>>>> GetWards(int districtId, int provinceId)
        {
            return await _addressService.GetWards(districtId, provinceId);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Address>>> AddAddress(Address address)
        {
            return await _addressService.AddAddress(address);
        }
    }
}

