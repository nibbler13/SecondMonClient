using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

namespace SecondMonClient {
	public partial class PopupMessage : Form, IMessageFilter {
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;
		public const int WM_LBUTTONDOWN = 0x0201;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		private HashSet<Control> controlsToMove = new HashSet<Control>();
		private Color colorMain;
		private Color colorExclamation;
		private int colorRSpeed;
		private int colorGSpeed;
		private int colorBSpeed;


		public PopupMessage() {
			Init();
		}

		public PopupMessage(string message) {
			Init();

			string[] values = message.Split('|');
			labelTitle.Text = values[0];
			labelMessage.Text = values[1];
			labelMark.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(values[2]));
			labelTitle.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(values[3]));
			labelMessage.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(values[3]));
			buttonClose.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(values[3]));

			buttonClose.Click += Form1_buttonClosed_Click;

			controlsToMove.Add(this);
			controlsToMove.Add(this.labelTitle);
			controlsToMove.Add(this.labelMessage);
			controlsToMove.Add(this.labelMark);

			int screenWidth = Screen.GetWorkingArea(this).Width;
			int screenHeight = Screen.GetWorkingArea(this).Height;

			Location = new System.Drawing.Point(screenWidth - Width - 20, screenHeight - Height - 20);

			colorMain = labelMessage.BackColor;
			colorExclamation = labelMark.BackColor;
			colorRSpeed = (colorExclamation.R - colorMain.R) / 20;
			colorGSpeed = (colorExclamation.G - colorMain.G) / 20;
			colorBSpeed = (colorExclamation.B - colorMain.B) / 20;

			StartBlinking();
		}

		private void Init() {
			Console.WriteLine("Form1 initialize");

			Application.AddMessageFilter(this);
			InitializeComponent();
		}

		private void StartBlinking() {
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 50;
			timer.Enabled = false;
			timer.Start();

			timer.Tick += new EventHandler(Timer_Tick);
		}

		private void Timer_Tick(object sender, EventArgs e) {
			if (labelMark.BackColor.R > colorExclamation.R &&
				labelMark.BackColor.G > colorExclamation.R &&
				labelMark.BackColor.B > colorExclamation.B) {
				labelMark.BackColor = Color.FromArgb(labelMark.BackColor.R + colorRSpeed, labelMark.BackColor.G + colorGSpeed, labelMark.BackColor.B + colorBSpeed);
			} else {
				labelMark.BackColor = colorMain;
			}
		}

		void Form1_buttonClosed_Click(Object sender, EventArgs e) {
			this.Close();
		}

		protected override CreateParams CreateParams {
			get {
				const int CS_DROPSHADOW = 0x20000;
				CreateParams cp = base.CreateParams;
				cp.ClassStyle |= CS_DROPSHADOW;
				return cp;
			}
		}

		public bool PreFilterMessage(ref Message m) {
			if (m.Msg == WM_LBUTTONDOWN &&
				controlsToMove.Contains(Control.FromHandle(m.HWnd))) {
				ReleaseCapture();
				SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
				return true;
			}

			return false;
		}
	}
}
