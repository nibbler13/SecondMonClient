using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace SecondMonClient {
    class SecondMonClient {
		
		static void Main(string[] args){
			try {
                while (true) {
					string hostName = Dns.GetHostName();

					IPAddress ipAd = null;

					foreach (IPAddress ipAddress in Dns.GetHostEntry(hostName).AddressList) {
						if (ipAddress.ToString().Contains("172."))
							ipAd = ipAddress;
					}

					if (ipAd == null) {
						throw new NullReferenceException("cannot find ipAddress");
					}

					TcpListener tcpListener = new TcpListener(ipAd, 8001);
                    tcpListener.Start();

                    Console.WriteLine("The server is running at port 8001");
                    Console.WriteLine("The local End point is " + tcpListener.LocalEndpoint);
                    Console.WriteLine("Waiting for a conncection");
                    Socket s = tcpListener.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                    Console.WriteLine("Received:");
                    string str = Encoding.Default.GetString(b);
                    Thread thread = new Thread(() => Startup(str));
					thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    s.Send(asen.GetBytes("The string was received by the server"));
                    Console.WriteLine("\nSent Acknowledgement");
                    s.Close();
                    tcpListener.Stop();
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message + " @ " + e.StackTrace);
            }

			Console.ReadLine();
        }

		
		static void Startup(string message){
			Console.WriteLine("startup");

			using (Form1 form = new Form1())
				Application.Run(form);

				//Application.EnableVisualStyles();
				//Application.SetCompatibleTextRenderingDefault(true);
				//Application.Run(new Form1());

				//MessageBox.Show(message,
				//	"Внимание!",
				//	MessageBoxButtons.OK,
				//	MessageBoxIcon.Asterisk,
				//	MessageBoxDefaultButton.Button1,
				//	MessageBoxOptions.ServiceNotification);

				Console.WriteLine("end startup");
		}
    }
}
