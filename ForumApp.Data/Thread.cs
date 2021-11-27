using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Data
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Forum))]
        public int ForumId { get; set; }
        public virtual Forum Forum { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        [Required]
        public DateTimeOffset ModifiedUtc { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Bookmark> Bookmarks { get; set; }
    }
}
