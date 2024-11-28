namespace Simple_Online_Survey_Application.Entity.Dtos
{
    public class PollCreateDto
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public List<string> Options { get; set; }
    }
}
