using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Nuts.ReportsGenerator
{
    class UdpSocket
    {
        private Socket _socket;
        private const int bufSize = 8 * 1024;
        private UdpState state;
        private EndPoint epFrom;
        private AsyncCallback recv = null;
        private const int StringLength = 130;

        public UdpSocket(string address, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));

            state = new UdpState();
            epFrom = new IPEndPoint(IPAddress.Any, 0);

        }

        public void Init()
        {
            Receive();
        }

        private void Receive()
        {
            _socket.BeginReceiveFrom(state.Buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                UdpState so = (UdpState)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                _socket.BeginReceiveFrom(so.Buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                NutStatus status = ByteArrayToObject(so.Buffer);
                Console.WriteLine("RECV: Data-  {2}-{3}-{4}-{5}", epFrom.ToString(), bytes, status.PData1, status.PData2, status.PData3, status.PData4);

            }, state);
        }

        private NutStatus ByteArrayToObject(byte[] arrBytes)
        {
            var status = new NutStatus();

            status.PData1 = BitConverter.ToUInt32(SubArray(arrBytes, 0, 4));
            status.PData2 = Encoding.ASCII.GetString(arrBytes, 4, StringLength);
            status.PData3 = BitConverter.ToUInt32(SubArray(arrBytes, StringLength+4, 4));
            status.PData4 = BitConverter.ToUInt32(SubArray(arrBytes, StringLength + 8, 4));

            return status;
        }

        private static byte[] SubArray(byte[] arr, int index, int length)
        {
            return arr.Skip(index).Take(length).ToArray();
        }

    }
}
