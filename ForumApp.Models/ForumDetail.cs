﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Models
{
    public class ForumDetail
    {
        public int ForumId { get; set; }
        public string Name { get; set; }
        public List<ThreadListItem> Threads { get; set; }
    }
}
