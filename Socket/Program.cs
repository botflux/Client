using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Socket
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new Exception("Aucun argument passé");
            
            Client client = new Client(IPAddress.Parse(args[0]), int.Parse(args[1]));

            for (;;)
            {
                Console.WriteLine("Entrer un message a envoyer:");
                string message = Console.ReadLine();
                client.Send(message);
            }
        }
    }
}
