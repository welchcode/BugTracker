using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class ProjectModel
    {
        public int ProjectId{ get; set; }
        public string ProjectDescription { get; set; }
        public DateTime DateUpdated { get; set; }
        public int BugCount { get; set; }
        public string ProjectName { get; set; }
    }
}
