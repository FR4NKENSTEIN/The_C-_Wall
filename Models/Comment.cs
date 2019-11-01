using System;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage="No empty Comments")]
        public string Content { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Message Message { get; set; }
        public User User { get; set; }

    }
    // I wonder how I to make Comments persist with a User's
    // list of Comments if I allow a Message to be deletable

    // You could use Self Joins to allow Comments to have Comments
}