using Simple_Online_Survey_Application.Entity;
using Simple_Online_Survey_Application.Entity.Dtos;
using Simple_Online_Survey_Application.Entity.Entities;

namespace Simple_Online_Survey_Application.Services.PollService
{
    public interface IPollService
    {
        Task<ServiceResponse<int>> CreatePollAsync(PollCreateDto pollDto, int appUserId);
        Task<ServiceResponse<List<Poll>>> GetAllPollsAsync();
        Task<ServiceResponse<List<Poll>>> GetAllMyPollsAsync(int appUserId);
        Task<ServiceResponse<Poll>> GetPollByIdAsync(int pollId, int appUserId);
    }
}
