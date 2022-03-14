using System;

namespace My.World.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Environment {get;set;}

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
