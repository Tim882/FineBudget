using DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;
using Data.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class IncomesController : BaseCrudController<Income, Guid, IncomeRequestDto, IncomeResponseDto>
    {
        public IncomesController(IBaseCrudDataService<Income, Guid, IncomeRequestDto, IncomeResponseDto> service)
            : base(service) { }
    }
}

