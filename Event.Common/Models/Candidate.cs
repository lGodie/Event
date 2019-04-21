namespace Event.Common.Models
{
    using System;
    using Newtonsoft.Json;
    public class Candidate
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("proposal")]
        public string Proposal { get; set; }

        [JsonProperty("imageUrl")]
        public object ImageUrl { get; set; }

    }
}
