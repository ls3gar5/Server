using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerConsoleApp
{
    public class ServerConsoleApp
    {
        const string DEFAULT_SERVER = "localhost";
        const int DEFAULT_PORT = 804;

        ////SERVER SOCKET STUFF
        //Socket serverSocket;
        //SocketInformation serverSocketInfo;
        IPEndPoint serverEndPoint;

        //public string Startup()
        //{
        //    //The chat server always starts up on the localhost, using the default port 
        //    IPHostEntry hostInfo = Dns.GetHostEntry(DEFAULT_SERVER);
        //    IPAddress serverAddr = hostInfo.AddressList.First(f => f.AddressFamily != AddressFamily.InterNetworkV6);
        //    var serverEndPoint = new IPEndPoint(serverAddr, DEFAULT_PORT);

        //    // Create a listener socket and bind it to the endpoint 
        //    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    serverSocket.Bind(serverEndPoint);

        //    return serverSocket.LocalEndPoint.ToString();
        //}


        public ServerConsoleApp()
        {
            //The chat server always starts up on the localhost, using the default port 
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(DEFAULT_SERVER);
                IPAddress serverAddr = hostInfo.AddressList.First(f => f.AddressFamily != AddressFamily.InterNetworkV6);
                serverEndPoint = new IPEndPoint(serverAddr, DEFAULT_PORT);
            }
            catch (System.Exception ex)
            {
                //Manejo de log
                throw ex;
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(ServerHandler));
        }


        private void ServerHandler(object state)
        {

            //// Create a listener socket and bind it to the endpoint 
            //serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //serverSocket.Bind(serverEndPoint);

            int backlog = 0;
            TcpListener listener = new TcpListener(serverEndPoint);

            listener.Start(backlog);
            System.Console.WriteLine("Server started - Listening on port: " + DEFAULT_PORT);

            var sock = listener.AcceptSocket();

            while (sock.Connected)
            {
                var buffer = new byte[1024];

                var dataReceive = sock.Receive(buffer);

                if (dataReceive == 0)
                {
                    break;
                }

                System.Console.WriteLine("Message Received...");

                var message = Encoding.ASCII.GetString(buffer);

                System.Console.WriteLine(message);
            }


            sock.Close();
            System.Console.WriteLine("Client Disconnected.");

           // listener.Stop();
           // System.Console.WriteLine("Server Stop.");

        }

        
    }
}
