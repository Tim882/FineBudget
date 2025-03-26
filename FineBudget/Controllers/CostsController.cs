using DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;
using Data.Service;
using FineBudget.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CostsController : BaseCrudController<Cost, Guid, CostRequestDto, CostResponseDto>
    {
        public CostsController(ICostDataService service)
            : base(service) { }
    }
}

