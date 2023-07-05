using ActionsPg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActionsPg;

namespace ActionsPg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDataController : ControllerBase
    {
        private readonly TestDataContext _context;

        public TestDataController(TestDataContext context)
        {
            _context = context;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<TestData>>> GetAsync()
        {
            var dataFromDb = await _context.TestData.ToListAsync();

            return Ok(dataFromDb);
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return new ContentResult
            {
                StatusCode = 400,
                Content = "Delete is not supported"
            };
        }
    }
}