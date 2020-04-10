using System;
using System.Collections.Generic;

namespace Nuts.ReportsGenerator.Entities
{
    [Serializable]
    public class IrClass : Parser
    {
        public UInt64 Var1 { get; set; }
        public UInt64 Var2 { get; set; }
        public UInt64 Var3 { get; set; }
        public UInt64 Var4 { get; set; }

        public override void DeSerializeByteArray(byte[] arr)
        {
            this.Var1 = BitConverter.ToUInt64(SubArray(arr, 0, 8));
            this.Var2 = BitConverter.ToUInt64(SubArray(arr, 8, 16));
            this.Var3 = BitConverter.ToUInt64(SubArray(arr, 16, 24));
            this.Var4 = BitConverter.ToUInt64(SubArray(arr, 24, 32));
        }
    }
}