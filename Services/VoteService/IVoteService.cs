using Simple_Online_Survey_Application.Entity.Dtos;
using Simple_Online_Survey_Application.Entity;

namespace Simple_Online_Survey_Application.Services.VoteService
{
    public interface IVoteService
    {
        Task<ServiceResponse<string>> CastVoteAsync(VoteDto voteDto, int appUserId);
        Task<ServiceResponse<PollResultDto>> GetPollResultsAsync(int pollId, int appUserId);
    }
}
