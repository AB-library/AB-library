using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; } = null;
        public string? Tag { get; set; } = null;
        public bool? ShowOnlyDrafts { get; set; }
    }
}   