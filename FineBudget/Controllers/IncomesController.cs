using Base.API;
using FineBudget.Data;
using FineBudget.DTO;
using FineBudget.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class IncomesController : BaseCrudController<Income, Guid, IncomeRequestDto, IncomeResponseDto>
    {
        public IncomesController(IIncomeDataService service)
            : base(service) { }
    }
}

