using Data.Service;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : BaseCrudController<Asset, Guid, AssetRequestDto, AssetResponseDto>
    {
        public AssetsController(IBaseCrudDataService<Asset, Guid, AssetRequestDto, AssetResponseDto> service)
            : base(service) { }
    }
}

