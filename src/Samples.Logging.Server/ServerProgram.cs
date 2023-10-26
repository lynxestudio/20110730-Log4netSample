using System.Net;
using System.Net.Sockets;
using System.IO;
using System;

namespace Samples.Logging.Server
{
    public class ServerProgram
    {
        public ServerProgram()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Loopback, 6060);
                listener.Start();
                Console.WriteLine("Waiting....");
                TcpClient tcpClient = listener.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();
                Console.WriteLine("Recibiendo....");
                using (StreamReader sr = new StreamReader(stream))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
                tcpClient.Close();
                listener.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Main(string[] args)
        {
            new ServerProgram();
            Console.ReadLine();
        }
    }
}