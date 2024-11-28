using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_Online_Survey_Application.Entity.Dtos;
using Simple_Online_Survey_Application.Services.PollService;

namespace Simple_Online_Survey_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePoll([FromBody] PollCreateDto dto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Invalid user ID.");
                }

                var response = await _pollService.CreatePollAsync(dto, appUserId);

                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(new { PollId = response.Data, Message = response.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPolls()
        {
            try
            {
                var response = await _pollService.GetAllPollsAsync();
                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("my-pools")]
        public async Task<IActionResult> GetMyPolls()
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Invalid user ID.");
                }

                var response = await _pollService.GetAllMyPollsAsync(appUserId);
                if (!response.Success)
                    return BadRequest(response.Message);

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("{pollId}")]
        public async Task<IActionResult> GetPollById(int pollId)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Invalid user ID.");
                }

                var response = await _pollService.GetPollByIdAsync(pollId, appUserId);

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
