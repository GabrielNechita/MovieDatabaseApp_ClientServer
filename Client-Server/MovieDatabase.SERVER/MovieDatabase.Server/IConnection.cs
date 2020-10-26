using System;
using System.Net.Sockets;

namespace MovieDatabase.Server
{
    public interface IConnection
    {
        Socket AcceptCallback(IAsyncResult ar, Socket serverSocket);
    }
}