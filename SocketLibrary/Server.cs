using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VPackage.Network
{
    public class Server
    {
        public event Action<string> OnMessageReceived;

        private UdpClient udpClient;
        private IPEndPoint endPoint;
        private bool finished = false;

        private Thread _listenTh;

        public Server (int listenPort)
        {
            udpClient = new UdpClient(listenPort);
            endPoint = new IPEndPoint(IPAddress.Any, listenPort);
            _listenTh = new Thread(new ThreadStart(Listen));
        }

        private void Listen ()
        {
            try
            {

                while (!finished)
                {
                    byte[] bytes = udpClient.Receive(ref endPoint);
                    if (OnMessageReceived != null) OnMessageReceived(Encoding.ASCII.GetString(bytes));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                udpClient.Close();
            }
        }

        public void StartListen ()
        {
            finished = false;
            _listenTh.Start();
        }

        public void StopListen()
        {
            finished = true;
        }
    }
}
