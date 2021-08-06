using System;
using System.Net.Sockets;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] buffer = new Byte[200];
            
            for (byte i = 0; i < buffer.Length; i++)
            {
                buffer[i] = i;
            }
            string ip = args[0];
            int port = int.Parse(args[1]);
            
            UdpClient udpClient = new UdpClient();
            
            while (true)
            {
                udpClient.Send(buffer, buffer.Length - 1,ip,port);
            }
        }
    }
}