using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ErrDetails { get; set; }

        public ErrorResponse()
        {
            
        }

        public ErrorResponse(string message, DateTime timeStamp, string errDetails)
        {
            Message = message;
            TimeStamp = timeStamp;
            ErrDetails = errDetails;
        }
    }
}
