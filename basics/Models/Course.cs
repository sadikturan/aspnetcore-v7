using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basics.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string[]? Tags { get; set; }
        public bool isActive { get; set; }
        public bool isHome { get; set; }

    }
}