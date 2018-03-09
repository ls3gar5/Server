using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static ServerConsoleApp.Mapped;

namespace ServerConsoleApp
{
    class Program
    {
        private static byte[] _buffer = new byte[1024];
        private static List<Socket> _clientSockets = new List<Socket>();
        private static Socket _serverSocet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string Path_Server = @"\\WIN7PROX64\HolistorW\Whapp";  //@"Z:\Whapp";
        public static string Ip_Name_Server;

        static void Main(string[] args)
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine();
            //if (String.IsNullOrWhiteSpace(Path_Server))
            //{
            //    throw new ArgumentNullException("La ruta es nula o son espacios en blanco.");
            //}

            //if (!Path.IsPathRooted(Path_Server))
            //{
            //    throw new ArgumentException(
            //        string.Format("La ruta '{0}' was not a rooted path and GetDriveLetter does not support relative paths."
            //        , Path_Server)
            //        );
            //}

            //if (Path_Server.StartsWith(@"\\"))
            //{
            //    //throw new ArgumentException("A UNC path was passed to GetDriveLetter");
            //    var tmpPathServer = Path_Server.Substring(2);//sacamos los \\ iniciales
            //    int ponint = tmpPathServer.IndexOf('\\'); //buscamos la primera barra donde va separa el resto de la ruta
            //    Ip_Name_Server = tmpPathServer.Substring(0, ponint); // Retornamos la IP
            //}
            //else
            //{
            //    Ip_Name_Server = Mapped.GetIpFromPath(Path_Server);
            //}


            //IPHostEntry ipHost = Dns.GetHostEntry(Ip_Name_Server);


            ////var Server = new ServerConsoleApp();
            //Console.ReadLine();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Server ON");


            _serverSocet.Bind(new IPEndPoint(IPAddress.Any, 804));
            _serverSocet.Listen(1); //Cuantas conexiones pendientes
            _serverSocet.BeginAccept(new AsyncCallback(AceeptCallback), null);
        }

        private static void AceeptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocet.EndAccept(AR);
            _clientSockets.Add(socket);
            Console.WriteLine("Client Connect");
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallback), socket);
            _serverSocet.BeginAccept(new AsyncCallback(AceeptCallback), null);
        }

        static int nroLlamada;
        static Random rnd = new Random();

        private static void RecieveCallback(IAsyncResult AR)
        {
            //_clientSockets.Add(socket); vienen de esa linea
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuf = new byte[received];
            Array.Copy(_buffer, dataBuf, received);

            //string text = Encoding.ASCII.GetString(dataBuf);
            string text = Encoding.Unicode.GetString(dataBuf,0,received);
            Console.WriteLine("texto: " + text);

            string reponse = string.Empty;

            if (text.ToLower() != "get time")
            {
                nroLlamada++;
                reponse = "Invaled Request " + nroLlamada.ToString();
            }
            else
            {
                reponse = DateTime.Now.ToLongTimeString();
            }

            //ACA CONSULTA AL SERVICIO///
            int time = rnd.Next(10000, 20000);
            Thread.Sleep(time);
            reponse += " " + time.ToString();

            //byte[] data = Encoding.ASCII.GetBytes(reponse);
            byte[] data = Encoding.Unicode.GetBytes(reponse);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallback), socket);
        }

        private static void SendCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
