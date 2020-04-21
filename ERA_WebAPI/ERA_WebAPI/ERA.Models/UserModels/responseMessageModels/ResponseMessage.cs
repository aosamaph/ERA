using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models.UserModels.responseMessage
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ExpireDate { get; set; }
        public IEnumerable<string> Errors { get; set; }
    
    }
}
