using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(2, ErrorMessage="Dawg, that's a letter not a name.")]
        public string FirstName {get;set;}

        [Required]
        [MinLength(2, ErrorMessage="Dawg, that's a letter not a name.")]
        public string LastName {get;set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password", ErrorMessage="Yo! That didn't match!")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

        public List<Message> Messages { get; set; }
        public List<Comment> Commentary { get; set; }
    }

    public class LogUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password { get; set; }
    }
}