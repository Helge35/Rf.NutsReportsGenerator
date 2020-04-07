using System;
using System.Collections.Generic;
using System.Text;

namespace Nuts.ReportsGenerator
{
    [Serializable]
    public class NutStatus
    {
        public UInt32 PData1 { get; set; }
        public string PData2 { get; set; }
        public UInt32 PData3 { get; set; }
        public UInt32 PData4 { get; set; }
    }
}
