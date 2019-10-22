using Core.DomainModel;
using Core.DomainModel.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.WebAPIService.Models
{
    public class RegistrationModel
    {

        #region Properties


        [Required(AllowEmptyStrings = false)]
        [JsonProperty("username")]
        public string UsertName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = Constant.Validation_StringLength_Min, MinimumLength = 6)]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("email")]
        [StringLength(50, ErrorMessage = Constant.Validation_StringLength_Max)]
        [RegularExpression(Constant.RegularExpression_Email, ErrorMessage = Constant.Validation_RegularExpression)]
        public string Email { get; set; }

        #endregion /Properties

        #region Construtors

        #endregion /Construtors

        #region Methods

        public User ConvertToUser() {
            return new User() {
                UserName = this.UsertName,
                NormalizedUserName = this.UsertName.ToUpper(),
                Email = this.Email,
                NormalizedEmail = this.Email.ToUpper(),
                EmailConfirmed = true,
                //PasswordHash =  this.Password,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                CustomTag = null
            };
        }

        #endregion /Methods

    }
}
