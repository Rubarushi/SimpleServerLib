using System;
using System.Net;
using System.Net.Sockets;

namespace ServerLib
{
    public class UdpServer
    {
        public UdpClient Client;
        private Action<byte[]> Process = null;
        private int m_iNeedLength = -1;
        public UdpServer(Action<byte[]> Processor, int port, int needLength = -1)
        {
            if (Equals(Processor, null))
            {
                throw new Exception("Methode Did Not Set");
            }
            
            m_iNeedLength = needLength;

            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, port);

            Client = new UdpClient(epRemote);

            Client.BeginReceive(ReceiveDone, null);
        }

        private void ReceiveDone(IAsyncResult ar)
        {
            IPEndPoint e = (IPEndPoint)ar.AsyncState;
            byte[] data = Client.EndReceive(ar, ref e);

            if(m_iNeedLength > 0 && m_iNeedLength < data.Length)
            {
                Client.BeginReceive(ReceiveDone, null);
            }
            Process.Invoke(data);
        }
    }
}
