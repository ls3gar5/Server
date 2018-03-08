using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Threading;

namespace ServerFromApp
{
    public partial class frmCliente : Form
    {
        SocketPermission permission;
        Socket sListener;
        IPEndPoint ipEndPoint;
        Socket handler;
        Dispatcher dispatcher;

        private static ManualResetEvent allDone = new ManualResetEvent(false);
        
        const string DEFAULT_SERVER = "";
        const int DEFAULT_PORT = 804;

        private TextBox tbAux = new TextBox();

        //public BackgroundWorker oWorkerBurn;
        //public delegate void FinalizoHandler(bool lExito);
        //public event FinalizoHandler finalizo;
        
        public frmCliente()
        {
            InitializeComponent();

            tbAux.TextChanged += TbAux_TextChanged;

            dispatcher = Dispatcher.CurrentDispatcher;

            StartServer();
            StartListen();
            //oWorkerBurn = new BackgroundWorker();
            //oWorkerBurn.WorkerSupportsCancellation = true;
            //oWorkerBurn.DoWork += OWorkerBurn_DoWork;
            //oWorkerBurn.RunWorkerCompleted += OWorkerBurn_RunWorkerCompleted;
        }
        
        //private void OWorkerBurn_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        // Creates one SocketPermission object for access restrictions
        //        permission = new SocketPermission(
        //            NetworkAccess.Accept,
        //            TransportType.Tcp,
        //            DEFAULT_SERVER,
        //            SocketPermission.AllPorts
        //            );

        //        // Ensures the code to have permission to access a Socket 
        //        permission.Demand();

        //        // Listening Socket object 
        //        sListener = null;

        //        // Resolves a host name to an IPHostEntry instance 
        //        IPHostEntry ipHost = Dns.GetHostEntry("");

        //        // Gets first IP address associated with a localhost 
        //        IPAddress ipAddr = ipHost.AddressList.First(f => f.AddressFamily != AddressFamily.InterNetworkV6);

        //        // Creates a network endpoint 
        //        ipEndPoint = new IPEndPoint(ipAddr, DEFAULT_PORT);

        //        // Create one Socket object to listen the incoming connection 
        //        sListener = new Socket(
        //            ipAddr.AddressFamily,
        //            SocketType.Stream,
        //            ProtocolType.Tcp);

        //        sListener.Bind(ipEndPoint);

        //        lblStatus.Text += "Server Conectado!!! + \n";


        //    }
        //    catch (Exception exc)
        //    {
        //        MessageBox.Show(exc.ToString());
        //    }

        //    try
        //    {
        //        // Places a Socket in a listening state and specifies the maximum 
        //        // Length of the pending connections queue 
        //        sListener.Listen(10);
        //        AsyncCallback aCallBack = new AsyncCallback(AccepCallBack);
        //        sListener.BeginAccept(aCallBack, sListener);

        //        lblStatus.Text += "Server is now listening on " + ipEndPoint.Address.ToString() + " port: " + ipEndPoint.Port.ToString() + "\n";

        //    }
        //    catch (Exception exc)
        //    {

        //        MessageBox.Show(exc.ToString());
        //    }
        //}
        
        //private void OWorkerBurn_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    onFinalizo(sender, e);
        //}

        //los dos método definidos en los eventos
        //public void onFinalizo(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (finalizo != null)
        //    {
        //        if (e.Error != null)
        //        {
        //            this.finalizo(false);
        //        }
        //        else
        //        {
        //            this.finalizo(((int)e.Result) == 0);
        //        }
        //    }
        //}


        private void TbAux_TextChanged(object sender, EventArgs e)
        {
            dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                txtMessageClient.Text = tbAux.Text;
            }
           );
        }


        //public bool StartServerApp()
        //{
        //    oWorkerBurn.RunWorkerAsync();

        //    return true;
        //}

        private void StartServer()
        {
            try
            {
                // Creates one SocketPermission object for access restrictions
                permission = new SocketPermission(
                    NetworkAccess.Accept,
                    TransportType.Tcp,
                    DEFAULT_SERVER,
                    SocketPermission.AllPorts
                    );

                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Listening Socket object 
                sListener = null;

                // Resolves a host name to an IPHostEntry instance 
                IPHostEntry ipHost = Dns.GetHostEntry("");

                // Gets first IP address associated with a localhost 
                IPAddress ipAddr = ipHost.AddressList.First(f => f.AddressFamily != AddressFamily.InterNetworkV6);

                // Creates a network endpoint 
                ipEndPoint = new IPEndPoint(ipAddr, DEFAULT_PORT);

                // Create one Socket object to listen the incoming connection 
                sListener = new Socket(
                    ipAddr.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);

                sListener.Bind(ipEndPoint);

                lblStatus.Text += "Server Conectado!!! + \n" ;


            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void StartListen()
        {
            try
            {
                // Places a Socket in a listening state and specifies the maximum 
                // Length of the pending connections queue 
                sListener.Listen(10);
                sListener.BeginAccept(new AsyncCallback(AccepCallBack), sListener);

                lblStatus.Text += "Server is now listening on " + ipEndPoint.Address.ToString() + " port: " + ipEndPoint.Port.ToString() + "\n";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        

        private void AccepCallBack(IAsyncResult ar)
        {
            Socket listener = null;
            // A new Socket to handle remote host communication 
            Socket handler = null;

            try
            {
                //Receiving byte array 
                byte[] buffer = new byte[1024];
                // Get Listening Socket object
                listener = (Socket)ar.AsyncState;
                // Create a new socket 
                handler = listener.EndAccept(ar);
                // Using the Nagle algorithm
                handler.NoDelay = false;

                //Creates one object array for passing data
                object[] obj = new object[2];
                obj[0] = buffer;
                obj[1] = handler;

                // Begins to asynchronously receive data
                handler.BeginReceive(
                    buffer,             // An array of type Byte for received data 
                    0,                  // The zero-based position in the buffer  
                    buffer.Length,      // The number of bytes to receive 
                    SocketFlags.None,   // Specifies send and receive behaviors 
                    new AsyncCallback(ReceiveCallback), //An AsyncCallback delegate 
                    obj                 // Specifies infomation for receive operation 
                    );

                // Begins an asynchronous operation to accept an attempt 
                listener.BeginAccept(new AsyncCallback(AccepCallBack), listener);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //Fetch a user-defined object that contains information
                object[] obj = new object[2];
                obj = (object[])ar.AsyncState;

                //Received byte array
                byte[] buffer = (byte[])obj[0];

                // A Socket to handle remote host communication. 
                handler = (Socket)obj[1];

                // Received message 
                string content = string.Empty;


                // The number of bytes received.
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    content += Encoding.Unicode.GetString(buffer, 0, bytesRead);

                    // If message contains "<Client Quit>", finish receiving
                    if (content.IndexOf("<Client Quit>") >-1)
                    {
                        // Convert byte array to string
                        string str = content.Substring(0, content.LastIndexOf("<Client Quit>"));

                        dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            tbAux.Text = "read " + str.Length * 2 + " bytes from client.\n Data: " + str;
                        });
                    }
                    else
                    {
                        // Continues to asynchronously receive data
                        byte[] buffernew = new byte[1024];
                        obj[0] = buffernew;
                        obj[1] = handler;
                        handler.BeginReceive(buffernew, 0, buffernew.Length,
                            SocketFlags.None,
                            new AsyncCallback(ReceiveCallback), obj);
                    }


                    dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        tbAux.Text = content;
                    });
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }


        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                // Convert byte array to string 
                string str = txtMsdToSend.Text;

                // Prepare the reply message 
                byte[] byteData =
                    Encoding.Unicode.GetBytes(str);

                // Sends data asynchronously to a connected Socket 
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);

            }
            catch (Exception exc )
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                // A Socket which has sent the data to remote host 
                Socket handler = (Socket)ar.AsyncState;

                // The number of bytes sent to the Socket 
                int bytesSend = handler.EndSend(ar);
                Console.WriteLine(
                    "Sent {0} bytes to Client", bytesSend);
            }
            catch (Exception exc) { MessageBox.Show(exc.ToString()); }
        }

        private void btnCloseConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (sListener.Connected)
                {
                    sListener.Shutdown(SocketShutdown.Receive);
                    sListener.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
