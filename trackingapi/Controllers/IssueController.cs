using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trackingapi.Data;

namespace trackingapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _dbContext;

        public IssueController(IssueDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
