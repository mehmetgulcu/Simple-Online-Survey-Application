namespace Simple_Online_Survey_Application.Entity.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; }
        public string OptionText { get; set; }
        public int VoteCount { get; set; }
    }
}
