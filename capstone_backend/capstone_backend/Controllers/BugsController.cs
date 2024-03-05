using capstone_backend.Models;
using capstone_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace capstone_backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BugsController : ControllerBase
  {
    private readonly IBugService _bugService;
    public BugsController(IBugService bugService)
    {
      _bugService = bugService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bugs>>> GetAllBugs()
    {
      var bugs = await _bugService.GetAllBugsAsync();
      return Ok(bugs);
    }

    // GET: api/Bugs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Bugs>>> GetBugById(int id)
    {
      List<Bugs> bug = await _bugService.GetBugByIdAsync(id);
      if (bug == null)
      {
        return NotFound();
      }
      return Ok(bug);
    }

    // POST: api/Bugs
    [HttpPost]
    public async Task<ActionResult<Bugs>> PostBug([FromBody] Bugs bug)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var createdBug = await _bugService.AddBugAsync(bug);
      return CreatedAtAction(nameof(GetBugById), new { id = createdBug.BugId }, createdBug);
    }

    // PUT: api/Bugs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBug(int id, [FromBody] Bugs bug)
    {
      if (id != bug.BugId)
      {
        return BadRequest();
      }

      await _bugService.UpdateBugAsync(bug);

      return NoContent();
    }

    // DELETE: api/Bugs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBug(int id)
    {
      await _bugService.DeleteBugAsync(id);
      return NoContent();
    }



    [HttpGet("total-bug-count")]
    public async Task<IActionResult> GetTotalBugCount()
    {
      try
      {
        var count = await _bugService.GetTotalBugCount();
        return Ok(count);
      }
      catch (Exception ex)
      {
        // Handle exception and return appropriate response
        return StatusCode(500, ex.Message);
      }
    }

    [HttpGet("severity/{severity}")]
    public async Task<IActionResult> GetBugCountBySeverity(string severity)
    {
      try
      {
        var bugCount = await _bugService.GetBugCountBySeverity(severity);
        return Ok(bugCount);
      }
      catch (Exception ex)
      {
        // Handle any exceptions that may occur during the operation
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the bug count by severity.");
      }
    }

    [HttpGet("severity/low")]
    public async Task<IActionResult> GetBugCountByLowSeverity()
    {
      try
      {
        var bugCount = await _bugService.GetBugCountByLowSeverity();
        return Ok(bugCount);
      }
      catch (Exception ex)
      {
        // Handle any exceptions that may occur during the operation
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the bug count by low severity.");
      }
    }

    [HttpGet("severity/medium")]
    public async Task<IActionResult> GetBugCountByMediumSeverity()
    {
      try
      {
        var bugCount = await _bugService.GetBugCountByMediumSeverity();
        return Ok(bugCount);
      }
      catch (Exception ex)
      {
        // Handle any exceptions that may occur during the operation
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the bug count by medium severity.");
      }
    }

    [HttpGet("severity/high")]
    public async Task<IActionResult> GetBugCountByHighSeverity()
    {
      try
      {
        var bugCount = await _bugService.GetBugCountByHighSeverity();
        return Ok(bugCount);
      }
      catch (Exception ex)
      {
        // Handle any exceptions that may occur during the operation
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the bug count by high severity.");
      }
    }
  }

}
