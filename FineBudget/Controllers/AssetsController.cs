using Data.Service;
using DTOs.Requests;
using DTOs.Responses;
using FineBudget.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : BaseCrudController<Asset, Guid, AssetRequestDto, AssetResponseDto>
    {
        public AssetsController(IAssetDataService service)
            : base(service) { }
    }
}

