using System;
using System.Collections.Generic;
using System.Text;

namespace ThailandPost
{
    public class GetTokenResponse
    {
        public string Token { get; set; }
        public DateTimeOffset? Expire { get; set; }
    }
}
