using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MicroService.AuthenticationService.Models
{
    public class PasswordChangeModel
    {

        #region Properties


        [Required(AllowEmptyStrings = false)]
        [JsonProperty("username")]
        public string UsertName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("oldpassword")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [JsonProperty("newpassword")]
        public string NewPassword { get; set; }

        #endregion /Properties

        #region Construtors

        #endregion /Construtors

    }
}
