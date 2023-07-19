using DataFormatter.Models;
using DataFormatter.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataFormatter.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class DataFormatterController : ControllerBase
  {
    private readonly IDataService _dataService;
    private readonly ILogger<DataFormatterController> _logger;

    public DataFormatterController(IDataService dataService, ILogger<DataFormatterController> logger)
    {
      _dataService = dataService;
      _logger = logger;      
    }

    [HttpPost]
    [Route("Data/FormatScheduleOfNoticesOfLeases")]
    public async Task<ActionResult<List<LeasescheduleDataOut>>> FormatScheduleOfNoticesOfLeases(List<LeasescheduleData> data)
    {
      return Ok(_dataService.GetMapLeaseScheduleData(data));
    }
  }
}
