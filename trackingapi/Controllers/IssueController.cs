using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trackingapi.Data;
using trackingapi.Models;

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

        [HttpGet]
        public async Task<IEnumerable<Issue>> Get()
        {
            return await _dbContext.Issues.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _dbContext.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Issue issue)
        {
            await _dbContext.Issues.AddAsync(issue);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = issue.Id}, issue);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Issue issue)
        {
            if (id != issue.Id) return BadRequest();

            _dbContext.Entry(issue).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _dbContext.Issues.FindAsync(id);
            if (issueToDelete == null) return NotFound();

            _dbContext.Issues.Remove(issueToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
