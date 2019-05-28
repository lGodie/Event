﻿namespace Event.Common.Models
{
    using Newtonsoft.Json;

    public partial class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString() => this.Name;
    }
}
