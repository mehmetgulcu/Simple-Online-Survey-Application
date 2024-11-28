namespace Simple_Online_Survey_Application.Entity.Entities
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Option> Options { get; set; }
    }
}
