using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Helpers
{
    public class PasswordHashingResult
    {
        public string PasswordHash { get; set; }
        public string SaltHash { get; set; }
    }
}
