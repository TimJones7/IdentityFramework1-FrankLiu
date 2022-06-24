using Newtonsoft.Json;

namespace IdentityFramework1_FrankLiu.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
