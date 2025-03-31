
using Newtonsoft.Json;

namespace DirectFerries.Models
{
    internal class AuthResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}
