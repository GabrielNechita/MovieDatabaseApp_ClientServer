using System;
using System.Net.Sockets;

namespace MovieDatabase.Server
{
    public class Connection : IConnection
    {
        public Socket AcceptCallback(IAsyncResult ar, Socket serverSocket)
        {         
            try
            {
                Socket socket = serverSocket.EndAccept(ar);
                return socket;
            }
            catch (ObjectDisposedException)
            {
                return null;
            }                      
        }
    }
}
