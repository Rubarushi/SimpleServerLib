# SimpleServerLib
A Simple server Library for C# (TCP, UDP)

# Usuage

## TCP
```cs
TcpServer<Client> server = new TcpServer<Client>(445);
server.BeginServer(() =>
{
    Console.WriteLine("New TcpServer Is Working on Port:445");
});
//or
server.BeginServer(null);

public class Client : IClient
{
    public void SetSocket(Socket sock)
    {
        //Add Process what you need
    }
}
```

## UDP
```cs
var proc = new UdpProcessior();
UdpServer udpServer = new UdpServer(proc.Process, 444, -1, () => Console.WriteLine("New UdpServer is Working on port:444"));
    
public class UdpProcessior : IUdpClient
{
    public void Process(byte[] mBuffer, IPEndPoint e)
    {
        //Add Process what you need
    }
}
```
