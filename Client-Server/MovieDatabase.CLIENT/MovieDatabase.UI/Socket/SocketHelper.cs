using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace MovieDatabase.UI.Socket
{
    public static class SocketHelper
    {
        private static readonly System.Net.Sockets.Socket ClientSocket = new System.Net.Sockets.Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;

        private static void ConnectToServer()
        {           
            while (!ClientSocket.Connected)
            {
                try
                {                   
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                }
            }

        }

        public static string ExecuteRequest(string requestText)
        {
            ConnectToServer();

            while (true)
            {
                SendRequest(requestText);

                return ReceiveResponse();
            }
        }

        private static void SendRequest(string request)
        {
            SendString(request);
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static string ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return "";
            var data = new byte[received];
            Array.Copy(buffer, data, received);

            return Encoding.ASCII.GetString(data);
        }
    }
}
