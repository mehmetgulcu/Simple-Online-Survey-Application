using Simple_Online_Survey_Application.Context;
using Simple_Online_Survey_Application.Entity.Dtos;
using Simple_Online_Survey_Application.Entity;
using Microsoft.EntityFrameworkCore;
using Simple_Online_Survey_Application.Entity.Entities;

namespace Simple_Online_Survey_Application.Services.VoteService
{
    public class VoteService : IVoteService
    {
        private readonly AppDbContext _context;

        public VoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<string>> CastVoteAsync(VoteDto voteDto, int appUserId)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var option = await _context.Options.Include(o => o.Poll).FirstOrDefaultAsync(o => o.Id == voteDto.OptionId);
                if (option == null)
                {
                    response.Success = false;
                    response.Message = "Option not found.";
                    return response;
                }

                var pollId = option.Poll.Id;

                var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.PollId == pollId && v.UserId == appUserId);
                if (existingVote != null)
                {
                    response.Success = false;
                    response.Message = "You have already voted in this poll.";
                    return response;
                }

                var vote = new Vote
                {
                    PollId = pollId,
                    UserId = appUserId,
                    OptionId = voteDto.OptionId
                };
                _context.Votes.Add(vote);

                option.VoteCount++;

                await _context.SaveChangesAsync();

                response.Data = "Vote registered successfully.";  // Wrap the success message in "data"
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while casting the vote: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<PollResultDto>> GetPollResultsAsync(int pollId, int appUserId)
        {
            var response = new ServiceResponse<PollResultDto>();
            try
            {
                var currentUser = await _context.Users.FindAsync(appUserId);

                if (currentUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }

                var poll = await _context.Polls.Include(p => p.Options).FirstOrDefaultAsync(p => p.Id == pollId);
                if (poll == null)
                {
                    response.Success = false;
                    response.Message = "Poll not found.";
                    return response;
                }

                var result = new PollResultDto
                {
                    PollTitle = poll.Title,
                    Results = poll.Options.Select(o => new PollOptionResult
                    {
                        OptionText = o.OptionText,
                        VoteCount = o.VoteCount
                    }).ToList()
                };

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving poll results: {ex.Message}";
            }

            return response;
        }
    }
}
