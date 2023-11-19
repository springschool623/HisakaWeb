using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HisakaWeb.Models
{
    public class LoginInfo
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Username must be at most 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
        public string Password { get; set; }
    }
}
