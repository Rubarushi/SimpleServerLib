using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerLib
{
    public class UdpServer
    {
        public UdpClient Client;
        private Action<byte[], IPEndPoint> Process = null;
        private int m_iNeedLength = -1;

        private Thread Worker = null;

        private volatile bool IsStopped = false;

        public UdpServer(Action<byte[], IPEndPoint> Processor, int port, int needLength = -1, Action MessageFunc = null)
        {
            if (Equals(Processor, null))
            {
                throw new Exception("Methode Did Not Set");
            }
            
            Process = Processor;
            m_iNeedLength = needLength;

            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, port);

            Client = new UdpClient(epRemote);

            Worker = new Thread(new ThreadStart(() => Client.BeginReceive(ReceiveDone, null)));
            Worker.Start();
            if (!object.Equals(null, MessageFunc))
            {
                MessageFunc.Invoke();
            }
        }

        private void ReceiveDone(IAsyncResult ar)
        {
            IPEndPoint e = (IPEndPoint)ar.AsyncState;
            byte[] data = Client.EndReceive(ar, ref e);

            if (m_iNeedLength > 0 && m_iNeedLength < data.Length)
            {
                Client.BeginReceive(ReceiveDone, null);
                return;
            }
            Process.Invoke(data, e);
            Client.BeginReceive(ReceiveDone, null);
        }

        public void StopUDP()
        {
            IsStopped = true;
            Worker.Join();
        }
    }
}
