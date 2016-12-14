using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SecondMonClient {
	public partial class Form1 : Form, IMessageFilter {
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;
		public const int WM_LBUTTONDOWN = 0x0201;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		private HashSet<Control> controlsToMove = new HashSet<Control>();


		public Form1() {
			Console.WriteLine("Form1 initialize");

			Application.AddMessageFilter(this);
			InitializeComponent();
			
			this.label2.Text = "У пациента в ближайшее время день рождения\nНе забудьте поставить скидку 10%";
			buttonClose.Click += Form1_buttonClosed_Click;

			controlsToMove.Add(this);
			controlsToMove.Add(this.label1);
			controlsToMove.Add(this.label2);
			controlsToMove.Add(this.label3);
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
