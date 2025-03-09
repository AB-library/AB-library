using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConspectFiles.Dto
{
    public class UpdateConspectDto
    {
        public string Title { get; set; }= string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}