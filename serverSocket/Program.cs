using System;
using System.Text;
using System.Net.Sockets;

namespace serverSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            //deprecated method
            TcpListener server = new TcpListener(8888);
            TcpClient client = default(TcpClient);

            int requestCount = 0;
            server.Start();
            Console.WriteLine(">> Server Started");
            client = server.AcceptTcpClient();
            Console.WriteLine(">> Accepted client connection\n");
            byte[] buffer = new byte[65536];

            while (true)
            {
                try
                {
                    requestCount++;
                    NetworkStream stream = client.GetStream();

                    stream.Read(buffer , 0 , (int)client.ReceiveBufferSize);

                    string data = Encoding.ASCII.GetString(buffer);
                    data = data.Substring(0 , data.IndexOf("$"));
                    Console.WriteLine("Client: " + data);

                    string response = "(Recieved)$";
                    byte[] sendBytes = Encoding.ASCII.GetBytes(response);
                    stream.Write(sendBytes , 0 , sendBytes.Length);

                    stream.Flush();
                }
                catch (Exception ex) {
                    Console.WriteLine("\n\tERROR: {0}\n", ex.Message);
                    client.Close();
                    server.Stop();
                    Console.WriteLine(">> EXITING...");
                    Console.ReadLine();
                    break;
                }
            }//while

        }
    }
}