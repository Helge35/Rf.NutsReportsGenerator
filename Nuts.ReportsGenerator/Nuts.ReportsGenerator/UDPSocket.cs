using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Nuts.ReportsGenerator.Entities;

namespace Nuts.ReportsGenerator
{
    class UdpSocket
    {
        private Socket _socket;
        private const int bufSize = 8 * 1024;
        private UdpState state;
        private EndPoint epFrom;
        private AsyncCallback recv = null;
        private const int StringLength = 32;

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
                Report report = new Report();
                report.DeSerializeByteArray(so.Buffer);
                Console.WriteLine("RECV: Data-  {2}-{3}-{4}", epFrom.ToString(), bytes, report.Var1, report.StrVar1, report.StrVar2);

            }, state);
        }

    }
}
