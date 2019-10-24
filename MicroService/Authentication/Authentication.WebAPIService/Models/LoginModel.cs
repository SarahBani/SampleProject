using Core.DomainModel;
using Core.DomainModel.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.WebAPIService.Models
{
    public class LoginModel
    {

        #region Properties


        [Required(AllowEmptyStrings = false)]
        [JsonProperty("username")]
        public string UsertName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }

        #endregion /Properties

    }
}
