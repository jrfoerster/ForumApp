using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class ThreadCreate
    {
        [Required]
        public int ForumId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string PostContent { get; set; }
    }
}
