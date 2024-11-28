using Simple_Online_Survey_Application.Context;
using Simple_Online_Survey_Application.Entity.Entities;
using Simple_Online_Survey_Application.Entity;
using Microsoft.EntityFrameworkCore;
using Simple_Online_Survey_Application.Entity.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Simple_Online_Survey_Application.Services.PollService
{
    public class PollService : IPollService
    {
        private readonly AppDbContext _context;

        public PollService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<int>> CreatePollAsync(PollCreateDto pollDto, int appUserId)
        {
            var response = new ServiceResponse<int>();

            try
            {
                var currentUser = await _context.Users.FindAsync(appUserId);

                if (currentUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }

                if (string.IsNullOrWhiteSpace(pollDto.Title) || pollDto.Options.Count < 2)
                {
                    response.Success = false;
                    response.Message = "Poll must have a title and at least two options.";
                    return response;
                }

                var poll = new Poll
                {
                    Title = pollDto.Title,
                    UserId = appUserId,
                    Options = pollDto.Options.Select(opt => new Option { OptionText = opt }).ToList()
                };

                _context.Polls.Add(poll);
                await _context.SaveChangesAsync();

                response.Data = poll.Id;
                response.Message = "Poll created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while creating the poll: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Poll>>> GetAllMyPollsAsync(int appUserId)
        {
            var response = new ServiceResponse<List<Poll>>();
            try
            {
                var currentUser = await _context.Users.FindAsync(appUserId);

                if (currentUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }

                var polls = await _context.Polls.Include(p => p.Options).Where(x => x.UserId == appUserId).ToListAsync();
                response.Data = polls;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving polls: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Poll>>> GetAllPollsAsync()
        {
            var response = new ServiceResponse<List<Poll>>();
            try
            {
                var polls = await _context.Polls.Include(p => p.Options).ToListAsync();
                response.Data = polls;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving polls: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<Poll>> GetPollByIdAsync(int pollId, int appUserId)
        {
            var response = new ServiceResponse<Poll>();
            try
            {
                var currentUser = await _context.Users.FindAsync(appUserId);

                if (currentUser == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }

                var poll = await _context.Polls
                    .Include(p => p.Options)
                    .FirstOrDefaultAsync(p => p.Id == pollId);

                if (poll == null)
                {
                    response.Success = false;
                    response.Message = "Poll not found.";
                    return response;
                }

                response.Data = poll;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving the poll: {ex.Message}";
            }

            return response;
        }
    }
}
