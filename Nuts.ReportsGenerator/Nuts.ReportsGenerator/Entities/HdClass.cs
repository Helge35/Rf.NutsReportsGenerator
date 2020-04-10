using System;
using System.Collections.Generic;

namespace Nuts.ReportsGenerator.Entities
{
    [Serializable]
    public class HdClass : Parser
    {
        public uint Var1 { get; set; }
        public uint Var2 { get; set; }
        public IrClass Ir { get; set; }


        public override void DeSerializeByteArray(byte[] arr)
        {
            this.Var1 = BitConverter.ToUInt32(SubArray(arr, 0, 4));
            this.Var2 = BitConverter.ToUInt32(SubArray(arr, 4, 8));

            this.Ir = new IrClass();
            this.Ir.DeSerializeByteArray(SubArray(arr, 8, 40));
        }
    }
}