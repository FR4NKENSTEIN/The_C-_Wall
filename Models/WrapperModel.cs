using System;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class WrapperModel
    {
        public Message Post { get; set; }
        public List<Message> AllMessages { get; set; }
    }
}