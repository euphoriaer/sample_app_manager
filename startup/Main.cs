using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace app_manager_startup
{
	public partial class Main : Form
	{
		public static FTPServer FTPServer;
		public Main()
		{
			InitializeComponent();
			InitLocalHost();
		}

		private void InitLocalHost()
		{
			string host = "https://" + GetIP() + ":5000";
			HostTextBox.Text = host;
		}

		private void StartUp_Click(object sender, EventArgs e)
		{
			Process cmd = new Process();

			cmd.StartInfo.FileName = "sample_app_manager.exe";
			cmd.StartInfo.Arguments = "--urls " + HostTextBox.Text;
			cmd.Start();
		}

		private string GetIP()
		{
			try
			{
				System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
				c.Connect("www.baidu.com", 80);
				string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.MapToIPv4().ToString();
				c.Close();
				return ip;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}