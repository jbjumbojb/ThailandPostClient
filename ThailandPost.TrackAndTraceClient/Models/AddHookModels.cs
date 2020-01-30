using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThailandPost
{
    public class AddHookResponse
    {
        public List<HookStatus> Items { get; set; }
        [JsonProperty(PropertyName = "track_count")]
        public TrackCount TrackCount { get; set; }
    }

    public class HookStatus
    {
        public string Barcode { get; set; }
        public bool Status { get; set; }
    }

}
