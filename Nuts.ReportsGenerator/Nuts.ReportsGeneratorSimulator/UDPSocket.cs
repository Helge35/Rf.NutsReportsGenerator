using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Nuts.ReportsGenerator.Entities;
using Nuts.ReportsGenerator.Simulator.Entities;

namespace Nuts.ReportsGenerator.Simulator
{
    class UdpSocketSender
    {
        private readonly Socket _socket;
        private readonly UdpState _state;
        private readonly PropertyInfo[] _props;

        public UdpSocketSender(string address, int port)
        {
            _state = new UdpState();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(IPAddress.Parse(address), port);

            _props = typeof(Report).GetProperties();
        }

        public void Send(Report report)
        {
            byte[] data = report.SerializeToByteArray();

            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                UdpState so = (UdpState)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, report.Var1);
            }, _state);
        }
    }
}
