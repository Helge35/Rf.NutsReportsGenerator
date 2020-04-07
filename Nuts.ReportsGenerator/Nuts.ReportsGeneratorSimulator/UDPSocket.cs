using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Nuts.ReportsGenerator.Simulator
{
    class UdpSocketSender
    {
        private readonly Socket _socket;
        private readonly UdpState _state;
        private const int StringLength = 130;
        private readonly PropertyInfo[] _props;

        public UdpSocketSender(string address, int port)
        {
            _state = new UdpState();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(IPAddress.Parse(address), port);

            _props = typeof(NutStatus).GetProperties();
        }

        public void Send(NutStatus status)
        {
            byte[] data = ObjectToByteArray(status);

            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                UdpState so = (UdpState)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, status.PData1);
            }, _state);
        }

        private byte[] ObjectToByteArray(NutStatus obj)
        {
            var bList = new List<byte>();
            foreach (var prop in _props)
            {
                var value = prop.GetValue(obj);
                switch (prop.PropertyType.Name)
                {
                    case (nameof(UInt32)):
                        bList.AddRange(BitConverter.GetBytes((UInt32)value));
                        break;
                    case (nameof(String)):
                        var strArr= Encoding.ASCII.GetBytes(value.ToString());

                        if (strArr.Length > StringLength)
                        {
                            Console.WriteLine("ERROR. Property:{0},   {1} -- Invalid string length", prop.Name, value);
                            return null;
                        }

                        var arr = new byte[StringLength];
                        Array.Clear(arr, 0, arr.Length);
                        Array.Copy(strArr, arr, strArr.Length);
                        bList.AddRange(arr);
                        break;
                    default:
                        Console.WriteLine("ERROR. Property:{0},   {1} -- Unsupported type {2}", prop.Name, value, prop.PropertyType.Name);
                        return null;
                }
            }
            return bList.ToArray();
        }
    }
}
