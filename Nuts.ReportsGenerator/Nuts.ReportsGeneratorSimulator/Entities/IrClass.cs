using System;
using System.Collections.Generic;

namespace Nuts.ReportsGenerator.Simulator.Entities
{
    [Serializable]
    public class IrClass : IParse
    {
        public UInt64 Var1 { get; set; }
        public UInt64 Var2 { get; set; }
        public UInt64 Var3 { get; set; }
        public UInt64 Var4 { get; set; }

        public byte[] SerializeToByteArray()
        {
            var bList = new List<byte>();
            bList.AddRange(BitConverter.GetBytes(this.Var1));
            bList.AddRange(BitConverter.GetBytes(this.Var2));
            bList.AddRange(BitConverter.GetBytes(this.Var3));
            bList.AddRange(BitConverter.GetBytes(this.Var4));
            return bList.ToArray();
        }
    }
}