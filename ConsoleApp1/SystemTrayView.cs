using SecondMonClient.Properties;
using System;
using System.Threading;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using System.IO;

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

			string iniFile = "FBSettings.ini";
			if (!File.Exists(iniFile)) {
				ShowErrorMessage(iniFile, "Отсутствует файл с настройками ");
				return;
			}

			string baseAddress = "";
			string baseName = "";
			try {
				FileIniDataParser iniDataReader = new FileIniDataParser();
				IniData iniData = iniDataReader.ReadFile(iniFile);

				string machineName = Environment.MachineName;
				if (machineName.Contains("-"))
					machineName = machineName.Split('-')[0].ToLower();

				baseAddress = iniData[machineName]["address"];
				baseName = iniData[machineName]["name"];
			} catch (Exception) {
				ShowErrorMessage(iniFile, "Не удалось получить значения ключей из файла с настройками ");
				return;
			}

			if (string.IsNullOrEmpty(baseAddress) ||
				string.IsNullOrEmpty(baseName)) {
				ShowErrorMessage(iniFile, "Ключи из настроек не содержат данных ");
				return;
			}
			
			checkNotificationSystem = new CheckNotificationsSystem(baseAddress, baseName);
			Thread thread = new Thread(() => checkNotificationSystem.CheckNotifications());
			thread.Start();
		}

		private void About(Object sender, EventArgs e) {
			MessageBox.Show("Эта программа предназначена для отображения уведомлений из инфоклиники");
		}

		private void ShowErrorMessage(string iniFile, string firstLine) {
			MessageBox.Show(firstLine + iniFile + Environment.NewLine +
				"Сообщите об этом в службу технической поддержки" + Environment.NewLine +
				"Контакты: внутренние номера 603 или 30-494, почта stp@bzklinika.ru",
				"Уведомление для врачей SecondMon",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
	}
}
