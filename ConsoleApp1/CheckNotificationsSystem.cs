using System;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SecondMonClient {
	class CheckNotificationsSystem {
		private const string QUERY_NEW_NOTIFICATIONS =
			"SELECT * FROM SECONDMONNOTIFICATIONS " +
			"WHERE IPADDRESS = @ipaddress " +
			"AND MISPID = @mispid " +
			"AND STATUS = 0 " +
			"AND cast(createdate as date ) = 'today' " +
			"and cast('now' as time) - cast(createdate as time) < 180";

		private const string QUERY_UPDATE_STATUS =
			"UPDATE SECONDMONNOTIFICATIONS " +
			"SET STATUS = 1 " +
			"WHERE SMNOTEID = @smnoteid";

		IPAddress[] ipAddresses;
		FBClient notificationBase;
		PopupMessage rootMessage;
	
		public CheckNotificationsSystem() {
			ipAddresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			notificationBase = new FBClient(
				Properties.Settings.Default.NOTIFICATION_BASE_IP_ADDRESS, 
				Properties.Settings.Default.NOTIFICATION_BASE_NAME);
			rootMessage = new PopupMessage();
			rootMessage.Show();
			rootMessage.Hide();
		}

		public void CheckNotifications() {
			while (true) {
				Thread.Sleep(Properties.Settings.Default.CYCLE_INTERVAL_IN_SEC * 1000);

				Process[] runningMisProcesses = Process.GetProcessesByName("infoclinica");
				if (runningMisProcesses.Length == 0)
					continue;

				int currentSessionID = Process.GetCurrentProcess().SessionId;
				Process[] currentUserMisProcesses = runningMisProcesses.
					Where(p => p.SessionId == currentSessionID).ToArray();
				if (currentUserMisProcesses.Length == 0)
					continue;

				foreach (IPAddress ipAddress in ipAddresses)
					foreach(Process misProcess in currentUserMisProcesses)
						CheckForNewNotifications(ipAddress.ToString(), misProcess.Id.ToString());
			}
		}

		private void CheckForNewNotifications(string ipAddress, string misPid) {
			Console.WriteLine("CheckForNewNotifications ipaddress: " + ipAddress + " misPid: " + misPid);
			if (ipAddress.Contains(":"))
				return;

			Dictionary<string, string> parameters = new Dictionary<string, string>() {
				{"@ipaddress", ipAddress},
				{"@mispid", misPid}};

			DataTable newNotifications = notificationBase.GetDataTable(QUERY_NEW_NOTIFICATIONS, parameters);
			if (newNotifications.Rows.Count == 0) {
				Console.WriteLine("--- table is empty");
			} else {
				foreach(DataRow row in newNotifications.Rows)
					ShowNotification(row);
			}
		}

		private void ShowNotification(DataRow row) {
			Console.WriteLine("--- ShowNotification");

			try {
				string id = row["SMNOTEID"].ToString();
				string title = row["TITLE"].ToString();
				string text = row["TEXT"].ToString();
				Color colorExclamationBlinking = ColorTranslator.FromHtml("#" + row["COLOREXT"].ToString());
				Color colorMain = ColorTranslator.FromHtml("#" + row["COLORMAIN"].ToString());
				Color colorFont = ColorTranslator.FromHtml("#" + row["COLORFONT"].ToString());

				Console.WriteLine("---id: " + id);
				
				Dictionary<string, string> parameters = new Dictionary<string, string>() {
					{"@smnoteid", id}};
				Console.WriteLine("---updated: " + notificationBase.ExecuteUpdateQuery(QUERY_UPDATE_STATUS, parameters));

				//color font
				Thread thread = new Thread(() => CreatePopupMessage(title, text, colorExclamationBlinking, colorMain));
				thread.Start();

				Console.WriteLine("---form closed");

			} catch (Exception e) {
				Console.WriteLine(e.Message + " " + e.StackTrace);
			}
		}

		private void CreatePopupMessage(string title, string text, Color colorExclamationBlinking, Color colorMain) {
			rootMessage.Invoke((MethodInvoker)delegate () {
				PopupMessage frm = new PopupMessage(title, text, colorExclamationBlinking, colorMain);
				frm.ShowDialog();
			});
		}
	}
}
