using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
    }
}
