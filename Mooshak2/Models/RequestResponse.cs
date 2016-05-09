using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Models
{
    public class RequestResponse
    {
        public string message { get; set; }
        public Status status { get; set; }
        public RequestResponse()
        {
            status = Status.Success;
            message = "Ok";
        }

        public RequestResponse(string message, Status status)
        {
            this.message = message;
            this.status = status;
        }
    }


    public enum Status
    {
        Success,
        Error
    }
}