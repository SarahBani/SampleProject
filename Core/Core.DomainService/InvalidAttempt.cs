﻿using System;

namespace Core.DomainServices
{
    public class InvalidAttempt
    {

        #region Properties

        public string IP { get; set; }

        //public string Action { get; set; }

        public int Count { get; set; }

        public DateTime DateTime { get; set; }

        #endregion /Properties

    }
}
