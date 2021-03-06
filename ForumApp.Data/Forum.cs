using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumApp.Data
{
    public class Forum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<Thread> Threads { get; set; }
    }
}
