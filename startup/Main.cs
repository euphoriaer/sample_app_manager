using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace app_manager_startup
{
	public partial class Main : Form
	{
		
		private Process app_manager;
		public Main()
		{
			InitializeComponent();
			InitLocalHost();
		}

		private void InitLocalHost()
		{
			string host = "https://" + GetIP() + ":5000";
			HostTextBox.Text = host;
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
			FtpPortText.Text = "443";
		}

		private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
		{
			if (app_manager != null)
			{
				app_manager.WaitForExitAsync();
			}
		}

		private void StartUp_Click(object sender, EventArgs e)
		{
			app_manager = new Process();

			app_manager.StartInfo.FileName = "sample_app_manager.exe";
			app_manager.StartInfo.Arguments = "--urls " + HostTextBox.Text + " --FTPport " + FtpPortText.Text;
			app_manager.Start();
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

		private void FtpPortText_TextChanged(object sender, EventArgs e)
		{

		}

		private void HostTextBox_TextChanged(object sender, EventArgs e)
		{

		}
	}
}