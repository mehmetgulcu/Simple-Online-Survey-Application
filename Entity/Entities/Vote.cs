namespace Simple_Online_Survey_Application.Entity.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
