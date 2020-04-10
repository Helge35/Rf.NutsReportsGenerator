using System;
using System.Collections.Generic;

namespace Nuts.ReportsGenerator.Simulator.Entities
{
    [Serializable]
    public class HdClass : IParse
    {
        public uint Var1 { get; set; }
        public uint Var2 { get; set; }
        public IrClass Ir { get; set; }

        public byte[] SerializeToByteArray()
        {
            var bList = new List<byte>();
            bList.AddRange(BitConverter.GetBytes(this.Var1));
            bList.AddRange(BitConverter.GetBytes(this.Var2));
            bList.AddRange(this.Ir.SerializeToByteArray());
            return bList.ToArray();
        }
    }
}