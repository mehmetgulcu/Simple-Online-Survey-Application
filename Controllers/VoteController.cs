using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_Online_Survey_Application.Entity.Dtos;
using Simple_Online_Survey_Application.Services.VoteService;

namespace Simple_Online_Survey_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost("vote")]
        public async Task<IActionResult> CastVote([FromBody] VoteDto voteDto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Invalid user ID.");
                }

                var response = await _voteService.CastVoteAsync(voteDto, appUserId);
                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(new { message = response.Data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("results/{pollId}")]
        public async Task<IActionResult> GetPollResults(int pollId)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Invalid user ID.");
                }

                var response = await _voteService.GetPollResultsAsync(pollId, appUserId);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
