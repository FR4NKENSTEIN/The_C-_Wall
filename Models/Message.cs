using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage="No empty Messages" )]
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public User Messenger { get; set; }
        public List<Comment> Comments { get; set; }
    }
}