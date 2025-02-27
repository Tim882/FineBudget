using DTOs.Responses;
using FineBudget.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FineBudget.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : Controller
    {
        private readonly ILogger<ImportController> _logger;
        private readonly IImportDataService _importDataService;

        public ImportController(ILogger<ImportController> logger, IImportDataService importDataService)
        {
            _logger = logger;
            _importDataService = importDataService;
        }

        [HttpPost("/ImportFromMoneyFlowCsv")]
        public async Task<IActionResult> ImportFromMoneyFlowCsv()
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                bool result = await _importDataService.ImportFromMoneyFlowCsv();

                response.Data = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
