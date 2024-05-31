using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Ticaret_API.Models
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public IEnumerable<string> ErrorCodes { get; set; }
    }
}
