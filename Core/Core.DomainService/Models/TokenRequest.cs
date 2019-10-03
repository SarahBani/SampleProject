using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainService.Models
{
    public class TokenRequest
    {

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
