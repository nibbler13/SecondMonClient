using SecondMonClient.Properties;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SecondMonClient {
	class SystemTrayView : ApplicationContext {
		private NotifyIcon trayIcon;
		CheckNotificationsSystem checkNotificationSystem;

		public SystemTrayView() {
			trayIcon = new NotifyIcon() {
				Icon = Resources.Icon1,
				ContextMenu = new ContextMenu(new MenuItem[] {
					new MenuItem("О программе", About)
				}),
				Visible = true
			};

			checkNotificationSystem = new CheckNotificationsSystem();
			Thread thread = new Thread(() => checkNotificationSystem.CheckNotifications());
			thread.Start();
		}

		private void About(Object sender, EventArgs e) {
			MessageBox.Show("Эта программа предназначена для отображения уведомлений из инфоклиники");
		}
	}
}
