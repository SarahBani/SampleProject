﻿namespace Core.DomainService.Settings
{
    public class AuthenticationAppSettings
    {

        public virtual string SecretKey { get; set; }

         public virtual string Audience { get; set; }

        public virtual string Issuer { get; set; }

    }
}
