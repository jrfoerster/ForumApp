using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class PostCreate
    {
        [Required]
        public int ThreadId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
