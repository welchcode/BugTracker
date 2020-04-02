using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class BugCommentModel
    {
        public string BugId { get; set; }
        public string BugComment { get; set; }
        public string Author { get; set; }
        public DateTime BugCommentDate { get; set; }
    }
}
