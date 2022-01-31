using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    public interface IClient
    {
        void SetSocket(Socket sock);
    }

    public interface IUdpClient
    {
        void Process(byte[] mBuffer);
    }
}
