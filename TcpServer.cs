using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerLib
{
    public class TcpServer<T> where T : IClient, new()
    {
        private TcpListener Listener = null;

        public delegate void Metho();

        private volatile bool IsStopped = false;

        private Thread Worker = null;

        public TcpServer(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);

            Worker = new Thread(new ThreadStart(() => {
                Listener.Start();
                Listener.BeginAcceptSocket(AcceptSocket, null);
            }));
        }

        public void BeginServer(Action MessageAction)
        {
            Worker.Start();

            if (!object.Equals(MessageAction, null))
            {
                MessageAction.Invoke();
            }
        }

        private void AcceptSocket(IAsyncResult IAR)
        {
            if(!IsStopped)
            {
                new T().SetSocket(Listener.EndAcceptSocket(IAR));
                Listener.BeginAcceptSocket(AcceptSocket, null);
            }
        }

        public void StopServer()
        {
            IsStopped = true;
            Worker.Join();
        }
    }
}
