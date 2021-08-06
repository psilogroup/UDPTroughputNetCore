using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace UDPTroughput
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = args[0];
            
            UdpClient receivingUdpClient = new UdpClient(int.Parse(port));

            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Stopwatch watch = new Stopwatch();
            long lastSecond = 0;
            long currentSecond = 0;
            long packets = 0;
            long packets2 = 0;
            long bytesRead = 0;
            long bytesRead2 = 0;
            int seconds = 0;
            Console.WriteLine("Waiting for packets");
            var dummy = receivingUdpClient.Receive(ref RemoteIpEndPoint);
            watch.Start();
            Console.WriteLine("Packet received, starting benchmark");
            Console.WriteLine("");
            
            while (seconds < 60)
            {
                Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                currentSecond = watch.ElapsedMilliseconds;
                packets++;
                bytesRead += receiveBytes.Length;
                
                if (currentSecond - lastSecond > 1000)
                {
                    lastSecond = currentSecond;
                    
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    var x = (packets - packets2);
                    var y = (bytesRead - bytesRead2);
                    Console.WriteLine("PPS: "+x+" - " + (y/(1024))+ " KB/s");
                    packets2 = packets;
                    bytesRead2 = bytesRead;
                    seconds++;

                }
            }
            Console.WriteLine($"Benchmark done: Mbs Received {bytesRead/(1024*1024)} Packets: {packets} Time: {seconds} seconds");
        }
    }
}