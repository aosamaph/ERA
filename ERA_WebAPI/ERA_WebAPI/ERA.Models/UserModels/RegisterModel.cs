using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models.UserModels
{
    public class RegisterModel
    {
       
        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        //[Required(ErrorMessage = "You must provide a phone number")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber{ get; set; }

        public string Address{ get; set; }

        public int? Age{ get; set; }

        public string ProfilePhoto{ get; set; }

        public Gender Gender { get; set; }



    }
}
