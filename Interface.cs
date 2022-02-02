using System.Net;
using System.Net.Sockets;

namespace ServerLib
{
    public interface IClient
    {
        void SetSocket(Socket sock);
    }

    public interface IUdpClient
    {
        void Process(byte[] mBuffer, IPEndPoint e);
    }
}
