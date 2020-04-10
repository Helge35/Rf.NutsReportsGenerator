using System;
using System.Collections.Generic;
using System.Text;
using Nuts.ReportsGenerator.Entities;

namespace Nuts.ReportsGenerator.Simulator.Entities
{
    [Serializable]
    public class Report
    {
        private const int StringLength = 32;

        public UInt64 Var1 { get; set; }
        public SystemStatusEnum SystemStatus { get; set; }
        public string StrVar1 { get; set; }
        public string StrVar2 { get; set; }
        public GdClass Gd { get; set; }
        public HdClass Hd { get; set; }

        public byte[] SerializeToByteArray()
        {
            var bList = new List<byte>();
            bList.AddRange(BitConverter.GetBytes(this.Var1));
            bList.AddRange(BitConverter.GetBytes((ulong)this.SystemStatus));
            bList.AddRange(ParseString(this.StrVar1));
            bList.AddRange(ParseString(this.StrVar2));
            bList.AddRange(this.Gd.SerializeToByteArray());
            bList.AddRange(this.Hd.SerializeToByteArray());
            return bList.ToArray();
        }

        private byte[] ParseString(string str)
        {
            var strArr = Encoding.ASCII.GetBytes(str);
            var arr = new byte[StringLength];
            Array.Clear(arr, 0, arr.Length);
            Array.Copy(strArr, arr, strArr.Length);
            return arr;
        }
    }
}
