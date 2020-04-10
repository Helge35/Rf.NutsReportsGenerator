using System.Linq;

namespace Nuts.ReportsGenerator.Entities
{
    public abstract class Parser
    {
        public abstract void DeSerializeByteArray(byte[] arr);

        protected byte[] SubArray(byte[] arr, int index, int length)
        {
            return arr.Skip(index).Take(length).ToArray();
        }

    }
}
