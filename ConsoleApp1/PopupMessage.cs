using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
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
		
		private double diffColorHue;
		private double diffColorSat;
		private double diffColorVal;

		private int stepsToChange = 30;
		private int currentStep = 0;

		public PopupMessage() {
			Init();
		}

		public PopupMessage(string title, string text, Color colorExclamationBlinking, Color colorMain) {
			Init();
			
			labelTitle.Text = title;
			labelMessage.Text = text;
			labelMark.BackColor = colorMain;
			labelTitle.BackColor = colorMain;
			labelMessage.BackColor = colorMain;
			buttonClose.BackColor = colorMain;

			buttonClose.Click += Form1_buttonClosed_Click;

			controlsToMove.Add(this);
			controlsToMove.Add(this.labelTitle);
			controlsToMove.Add(this.labelMessage);
			controlsToMove.Add(this.labelMark);

			int screenWidth = Screen.GetWorkingArea(this).Width;
			int screenHeight = Screen.GetWorkingArea(this).Height;

			Location = new System.Drawing.Point(screenWidth - Width - 20, screenHeight - Height - 20);

			double mainColorHue;
			double mainColorSat;
			double mainColorVal;
			double exclColorHue;
			double exclColorSat;
			double exclColorVal;

			ColorToHSV(colorMain, out mainColorHue, out mainColorSat, out mainColorVal);
			ColorToHSV(colorExclamationBlinking, out exclColorHue, out exclColorSat, out exclColorVal);

			diffColorHue = (exclColorHue - mainColorHue) / stepsToChange;
			diffColorSat = (exclColorSat - mainColorSat) / stepsToChange;
			diffColorVal = (exclColorVal - mainColorVal) / stepsToChange;

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
			double curHue;
			double curSat;
			double curVal;
			ColorToHSV(labelMark.BackColor, out curHue, out curSat, out curVal);

			curHue += diffColorHue;
			curSat += diffColorSat;
			curVal += diffColorVal;

			if (curHue < 0)
				curHue = 0;
			if (curHue > 360)
				curHue = 360;
			if (curSat < 0)
				curSat = 0;
			if (curSat > 1)
				curSat = 1;
			if (curVal < 0)
				curVal = 0;
			if (curVal > 1)
				curVal = 1;

			labelMark.BackColor = ColorFromHSV(curHue, curSat, curVal);

			currentStep++;
			if (currentStep == stepsToChange) {
				diffColorHue *= -1;
				diffColorSat *= -1;
				diffColorVal *= -1;
				currentStep = 0;
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

		private static void ColorToHSV(Color color, out double hue, out double saturation, out double value) {
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			value = max / 255d;
		}

		private static Color ColorFromHSV(double hue, double saturation, double value) {
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0)
				return Color.FromArgb(255, v, t, p);
			else if (hi == 1)
				return Color.FromArgb(255, q, v, p);
			else if (hi == 2)
				return Color.FromArgb(255, p, v, t);
			else if (hi == 3)
				return Color.FromArgb(255, p, q, v);
			else if (hi == 4)
				return Color.FromArgb(255, t, p, v);
			else
				return Color.FromArgb(255, v, p, q);
		}
	}
}
