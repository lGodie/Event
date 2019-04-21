namespace Event.Common.Models
{
    using Newtonsoft.Json;
    public class Vote
    {
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("candidate")]
        public Candidate Candidate { get; set; }
    }
}
