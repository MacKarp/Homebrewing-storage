using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        public string UserNormalizedName 
        {
            get
            {
                return UserName.ToUpper();
            }
            set { 
                    UserName.ToUpper(); 
                } 
        }
        public string UserNormalizedEmail
        {
            get
            {
                return UserEmail.ToUpper();
            }
            set
            {
                UserEmail.ToUpper();
            }
        }

        public bool UserLockoutEnabled { get; set; } = true;

    }
}
