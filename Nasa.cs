using System;
using System.Text.Json.Serialization;

namespace V_secretary_bot
{
    public class Nasa
    {

        public string? url { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }

    }
}
