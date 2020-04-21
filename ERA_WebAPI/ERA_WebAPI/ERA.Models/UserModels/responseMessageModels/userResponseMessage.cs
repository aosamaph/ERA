using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models.UserModels.responseMessage
{
    public class userResponseMessage : ResponseMessage
    {
        public RegisterModel user { get; set; }
    }
}
