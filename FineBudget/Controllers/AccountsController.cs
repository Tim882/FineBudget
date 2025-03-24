using Data.Service;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BaseCrudController<Account, Guid, AccountRequestDto, AccountResponseDto>
    {
        public AccountsController(IBaseCrudDataService<Account, Guid, AccountRequestDto, AccountResponseDto> service)
            : base(service) { }
    }
}

