using Data.Service;
using DTOs;
using FineBudget.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels.MainModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BaseCrudController<Account, Guid, AccountRequestDto, AccountResponseDto>
    {
        public AccountsController(IAccountDataService service)
            : base(service) { }
    }
}

