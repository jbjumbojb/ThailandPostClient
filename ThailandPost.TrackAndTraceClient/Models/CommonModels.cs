using System;
using System.Collections.Generic;
using System.Text;

namespace ThailandPost
{
    public enum Language
    {
        TH,EN,CN
    }
    public enum Status
    {
        All=0,
        PrepareToDrop=101,
        DropAtAgent=102,
        Droped=103,
        Transporting=201,
        PerformCustoms=202,
        ReturnToSender=203,
        Sending=301,
        SentAtDroppoint=302,
        Failed=401,
        Success=501,
        OutboundExchange=204,
        InboundExchange=205,
        AtPostOffice=206,
        PrepareToSend=207
    }
    public class ResponseResult<T> where T : class
    {
        public T Response { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}