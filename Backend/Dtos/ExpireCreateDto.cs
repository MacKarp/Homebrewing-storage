using System;
using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos
{
    public class ExpireCreateDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int IdStorage { get; set; } 
        [Required]
        public int IdItem { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}