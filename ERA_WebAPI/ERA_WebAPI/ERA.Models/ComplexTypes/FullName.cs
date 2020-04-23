using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    [ComplexType]
    public class FullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
