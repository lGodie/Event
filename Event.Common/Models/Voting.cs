namespace Event.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Voting
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("dateTimeStart")]
        public DateTime? DateTimeStart { get; set; }

        [JsonProperty("dateTimeEnd")]
        public DateTime? DateTimeEnd { get; set; }

        [JsonProperty("isEnableBlankVote")]
        public bool IsEnableBlankVote { get; set; }

        [JsonProperty("quantityVotes")]
        public long QuantityVotes { get; set; }

        [JsonProperty("quantityBlankVotes")]
        public long QuantityBlankVotes { get; set; }

        [JsonProperty("candidateWinId")]
        public long CandidateWinId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public override string ToString()
        {
            return $"{this.Description} {this.Remarks}";
        }

        [JsonProperty("candidates")]
        public List<Candidate> Candidates { get; set; }
    }
}
