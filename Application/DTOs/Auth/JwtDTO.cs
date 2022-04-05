using System;

namespace Application.DTOs
{
    public class JwtDTO
    {
        public string Token { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
