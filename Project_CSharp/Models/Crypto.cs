using System;
using System.Collections.Generic;

namespace Project_CSharp.Models
{
    public class Crypto
    {
        public DateTime Time { get; set; }
        public string Base { get; set; }
        public string Quote  { get; set; }
        public decimal Rate { get; set; }
        public ICollection<SrcSideBase> SrcSideBases { get; set; }
    }
}
