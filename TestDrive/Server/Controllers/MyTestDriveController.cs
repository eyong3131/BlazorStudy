using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestDrive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MyTestDriveController : ControllerBase
    {
        private readonly ITestDriveService _testDriveService;
        public MyTestDriveController(ITestDriveService testDriveService)
        {
            _testDriveService = testDriveService;
        }
        [HttpGet]
        public async Task<ActionResult<List<MiddleWare>>> GetAll()
        {
            return Ok(await _testDriveService.GetAll());
        }
    }
}