using System;
using System.Windows.Forms;

namespace SecondMonClient {
    class SecondMonClient {

		[STAThread]
		static void Main(string[] args){
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SystemTrayView());
        }
    }
}
