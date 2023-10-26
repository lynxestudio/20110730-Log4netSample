using System;
using System.Net.Sockets;
using System.IO;
using System.Net;
using log4net;
using log4net.Config;
using log4net.Appender;
using System.Configuration;

namespace Samples.Logging.Client
{
    class Program
    {
        static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Program()
        {
            try
            {
                Console.Write("Puerto > ");
                int thePort = Convert.ToInt32(Console.ReadLine());
                Console.Write("File Name >");
                string FileName = Console.ReadLine();
                string s = null;
                TcpClient c = new TcpClient("127.0.0.1", thePort);
                NetworkStream stream = c.GetStream();
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    Console.WriteLine("Leyendo archivo");
                    using (FileStream fs = new FileStream(FileName, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            while ((s = sr.ReadLine()) != null)
                            {
                                writer.WriteLine(s);
                            }
                        }
                    }
                }
                stream.Close();
                c.Close();
            }
            catch (FormatException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (SocketException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (IOException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        public static void Main(string[] args)
        {
            try
            {
                log.Info("Ejecutando la aplicación en " + DateTime.Now.ToLongTimeString());
                XmlConfigurator.Configure();
                new Program();
                log.Info("Cerrando aplicación en " + DateTime.Now.ToLongTimeString());
            }
            catch (ConfigurationException ex)
            {
                Console.WriteLine("No se pudo cargar el logger", ex);
            }
            catch (Exception)
            {
                Console.WriteLine("Hubo un error, ver el log");
            }
        }
    }
}