using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
