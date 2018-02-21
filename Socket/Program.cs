using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using VPackage.Network;
using System.Threading;

namespace SocketLibrary
{
    class Program
    {
        private static bool messageReceived = false;

        public static void ReceiveCallBack (IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).U;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).E;

            byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("Received: {0}", receiveString);
            messageReceived = true;
        }

        static void Main(string[] args)
        {

            IPEndPoint e = new IPEndPoint(IPAddress.Any, 15200);
            UdpClient u = new UdpClient(e);

            UdpState s = new SocketLibrary.UdpState();

            s.E = e;
            s.U = u;

            Console.WriteLine("listening for messages");

            u.BeginReceive(new AsyncCallback(ReceiveCallBack), s);
            /*
            while (!messageReceived)
            {
                Thread.Sleep(100);
            }*/

            Client client = new Client("127.0.0.1", 15200);
            client.Send("Hello world");

            Console.ReadKey();

            /*
            Client client = new Client("10.129.21.245", 5000);
            Server server = new Server(15000);
            server.OnMessageReceived += ((message) => 
            {
                Console.WriteLine(message);
            });

            server.StartListen();
            
            

            while (Console.ReadKey().Key != ConsoleKey.S)
            {
                Console.WriteLine("Entrer un message a envoyer:");
                string message = Console.ReadLine();
                client.Send(message);
            }
            server.StopListen();*/
        }
    }
}
