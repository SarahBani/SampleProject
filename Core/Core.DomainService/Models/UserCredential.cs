using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.DomainService.Models
{
    public class UserCredential 
    {

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
