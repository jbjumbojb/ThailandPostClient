using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThailandPost
{
    public class GetItemsRequest
    {
        public string Status { get; set; }
        public string Language { get; set; }
        public IEnumerable<string> Barcode { get; set; }
    }
    public class GetItemsResponse
    {
        public Dictionary<string, List<TrackStatus>> Items { get; set; }
        [JsonProperty(PropertyName = "track_count")]
        public TrackCount TrackCount { get; set; }
    }

    public class TrackStatus
    {
        public string Barcode { get; set; }
        public string Status { get; set; }
        [JsonProperty(PropertyName = "status_description")]
        public string StatusDescription { get; set; }
        [JsonProperty(PropertyName = "status_date")]
        public string StatusDate { get; set; }
        public string Location { get; set; }
        public string Postcode { get; set; }
        [JsonProperty(PropertyName = "delivery_status")]
        public string DeliveryStatus { get; set; }
        [JsonProperty(PropertyName = "delivery_description")]
        public string DeliveryDescription { get; set; }
        [JsonProperty(PropertyName = "delivery_datetime")]
        public string DeliveryDatetime { get; set; }
        [JsonProperty(PropertyName = "receiver_name")]
        public string ReceiverName { get; set; }
        public string Signature { get; set; }
    }

   
}
