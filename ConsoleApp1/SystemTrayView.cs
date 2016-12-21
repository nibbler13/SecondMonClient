using SecondMonClient.Properties;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SecondMonServer;

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

					byte[] lengthToReceive = new byte[4];
					s.Receive(lengthToReceive);

					byte[] receivedData = new byte[BitConverter.ToInt32(lengthToReceive, 0)];
					s.Receive(receivedData);

					MemoryStream stream = new MemoryStream(receivedData);
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Binder = new DeserializationBinder();
					stream.Position = 0;
					object rawObj = formatter.Deserialize(stream);
					Notification notification = (Notification)rawObj;

					//Console.WriteLine("Received:");
					//string str = Encoding.Default.GetString(b);




					ASCIIEncoding asen = new ASCIIEncoding();
					s.Send(asen.GetBytes("The string was received by the server"));
					Console.WriteLine("\nSent Acknowledgement");

					s.Close();
					tcpListener.Stop();

					//string result = Encoding.Unicode.GetString(receivedData);

					Thread thread = new Thread(() => Startup(notification));
					thread.Start();
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message + " @ " + e.StackTrace);
			}
		}
		
		private void Startup(Notification notification) {
			Console.WriteLine("---startup");

			this.message.Invoke((MethodInvoker)delegate () {
				PopupMessage frm = new PopupMessage(notification);
				frm.ShowDialog();
			});

			Console.WriteLine("---form closed");
		}
	}
}
