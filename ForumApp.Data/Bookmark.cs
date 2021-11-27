using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumApp.Data
{
    public class Bookmark
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Thread))]
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
