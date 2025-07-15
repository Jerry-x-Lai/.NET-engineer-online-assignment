using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace CryptoInfoApi.Models
{
    public class CryptoInfoResult
    {
        public string UpdatedTime { get; set; } = string.Empty;
        public List<CryptoInfoCurrency> Currencies { get; set; } = new();
    }

    public class CryptoInfoCurrency
    {
        public string Code { get; set; } = string.Empty;
        public string ChineseName { get; set; } = string.Empty;
        public decimal Rate { get; set; }
    }
}
