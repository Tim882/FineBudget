using Base.API;
using FineBudget.Data;
using FineBudget.DTO;
using FineBudget.Models;
using Microsoft.AspNetCore.Mvc;

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

