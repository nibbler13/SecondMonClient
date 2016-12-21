using SecondMonClient.Properties;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SecondMonClient {
	class SystemTrayView : ApplicationContext {
		private NotifyIcon trayIcon;
		private PopupMessage message;

		public SystemTrayView() {
			trayIcon = new NotifyIcon() {
				Icon = Resources.Icon1,
				ContextMenu = new ContextMenu(new MenuItem[] {
					new MenuItem("О программе", About)
				}),
				Visible = true
			};

			message = new PopupMessage();
			message.Show();
			message.Hide();

			RunNetworkListener();
		}

		private void About(Object sender, EventArgs e) {
			MessageBox.Show("Эта программа предназначена для отображения уведомлений из инфоклиники");
		}

		private void RunNetworkListener() {
			string hostName = Dns.GetHostName();

			foreach (IPAddress ipAddress in Dns.GetHostEntry(hostName).AddressList) {
				Thread thread = new Thread(() => StartTcpClient(ipAddress));
				thread.Start();
			}
		}

		private void StartTcpClient(IPAddress ipAddr) {
			try {
				while (true) {
					if (ipAddr == null) {
						throw new NullReferenceException("cannot find ipAddress");
					}

					TcpListener tcpListener = new TcpListener(ipAddr, 8001);
					tcpListener.Start();

					Console.WriteLine("The local End point is " + tcpListener.LocalEndpoint);
					Console.WriteLine("Waiting for a conncection");
					Socket s = tcpListener.AcceptSocket();
					Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

					byte[] length = new byte[4];
					int received = s.Receive(length);

					byte[] b = new byte[BitConverter.ToInt32(length, 0)];
					int k = s.Receive(b);

					//Console.WriteLine("Received:");
					//string str = Encoding.Default.GetString(b);


					ASCIIEncoding asen = new ASCIIEncoding();
					s.Send(asen.GetBytes("The string was received by the server"));
					Console.WriteLine("\nSent Acknowledgement");
					s.Close();
					tcpListener.Stop();

					string result = Encoding.Unicode.GetString(b);

					Thread thread = new Thread(() => Startup(result));
					thread.Start();
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message + " @ " + e.StackTrace);
			}
		}
		
		private void Startup(string text) {
			Console.WriteLine("---startup");

			this.message.Invoke((MethodInvoker)delegate () {
				PopupMessage frm = new PopupMessage(text);
				frm.ShowDialog();
			});

			Console.WriteLine("---form closed");
		}
	}
}
