using System.Text.Json.Serialization;

namespace CryptoInfoApi.Models
{
    public class CoindeskBpiCurrency
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("rate")]
        public string Rate { get; set; } = string.Empty;
        [JsonPropertyName("rate_float")]
        public decimal RateFloat { get; set; }
    }

    public class CoindeskBpi
    {
        [JsonPropertyName("USD")]
        public CoindeskBpiCurrency? USD { get; set; }
        [JsonPropertyName("GBP")]
        public CoindeskBpiCurrency? GBP { get; set; }
        [JsonPropertyName("EUR")]
        public CoindeskBpiCurrency? EUR { get; set; }
    }

    public class CoindeskTime
    {
        [JsonPropertyName("updatedISO")]
        public string UpdatedISO { get; set; } = string.Empty;
    }

    public class CoindeskResponse
    {
        [JsonPropertyName("time")]
        public CoindeskTime Time { get; set; } = new();
        [JsonPropertyName("bpi")]
        public CoindeskBpi Bpi { get; set; } = new();
    }
}
