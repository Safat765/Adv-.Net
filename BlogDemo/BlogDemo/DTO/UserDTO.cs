using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogDemo.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username required!")]
        [StringLength(25, MinimumLength = 5)]
        [RegularExpression(@"[^\s]+", ErrorMessage = "Username cannot have any space")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password required!")]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z\s]).{8,50}$", ErrorMessage = "Password must be between 8 and 50 characters in length, contain at least 1 capital letter, 1 small letter, 1 special character (no spaces), and 1 number.")]
        public string Password { get; set; }
    }
}