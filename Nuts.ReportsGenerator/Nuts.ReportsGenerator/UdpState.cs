using System;
using System.Collections.Generic;
using System.Text;

namespace Nuts.ReportsGenerator
{
    public class UdpState
    {

        public UdpState()
        {
            int bufSize = 8 * 1024;
            Buffer = new byte[bufSize];
        }

        public byte[] Buffer { get; set; }
    }
}
