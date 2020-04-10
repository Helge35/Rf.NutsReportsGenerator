using System;
using System.Collections.Generic;
using System.Text;

namespace Nuts.ReportsGenerator.Entities
{
    [Serializable]
    public class Report : Parser
    {
        private const int StringLength = 32;

        public UInt64 Var1 { get; set; }
        public SystemStatusEnum SystemStatus { get; set; }
        public string StrVar1 { get; set; }
        public string StrVar2 { get; set; }
        public GdClass Gd { get; set; }
        public HdClass Hd { get; set; }

        public override void DeSerializeByteArray(byte[] arr)
        {
            int index = 0;
            this.Var1 = BitConverter.ToUInt64(SubArray(arr, 0, index += 8));
            this.SystemStatus =(SystemStatusEnum)BitConverter.ToUInt64(SubArray(arr, index, index += 8));
            this.StrVar1 = Encoding.ASCII.GetString(arr, index, index+=StringLength);
            this.StrVar2 = Encoding.ASCII.GetString(arr, index, index += StringLength);

            this.Gd = new GdClass();
            this.Gd.DeSerializeByteArray(SubArray(arr, index, index+=40));

            this.Hd = new HdClass();
            this.Hd.DeSerializeByteArray(SubArray(arr, index, index += 40));
        }


    }
}
