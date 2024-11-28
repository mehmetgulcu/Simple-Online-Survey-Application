namespace Simple_Online_Survey_Application.Entity.Dtos
{
    public class PollResultDto
    {
        public string PollTitle { get; set; }
        public List<PollOptionResult> Results { get; set; }
    }

}
