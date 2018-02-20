using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using VPackage.Network;

namespace SocketLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
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
            server.StopListen();
        }
    }
}
