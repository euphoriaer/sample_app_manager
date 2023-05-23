using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace SampleAppManager.FTPServer
{
	public class FTPServerProvide
	{
		protected NavigationManager NavigationManager { get; }
		private string url;
		private static FTPServer FTPServer;
		public static string wwwroot;
		public static string envWebRootPath;
		public static string envContentRootPath;
		public FTPServerProvide(NavigationManager navigationManager)
		{
			if (FTPServer == null)
			{
				NavigationManager = navigationManager;
				url = NavigationManager.BaseUri;
				wwwroot = Path.Combine(Environment.CurrentDirectory, "wwwroot");
				envWebRootPath = wwwroot;
				envContentRootPath = Environment.CurrentDirectory;
				var serverConfig = Path.Combine(envContentRootPath, "config.yaml");

				var folder = Path.Combine(envWebRootPath, "files");
				var serverPath = Path.Combine(envWebRootPath, "ftp_server", "hfs.exe");
				FTPServer = new FTPServer(folder, serverPath, serverConfig);
			}
		}

		public void Init()
		{

		}

	}



	public class FTPServer
	{


		public Process serverProcess;

		public FTPServer(string savePath, string serverPath, string configPath)
		{

			if (File.Exists(configPath))
			{
				File.Delete(configPath);
			}
			string cfg = "vfs:\r\n  " +
				"children:\r\n    " +
				$"- source: {FTPServerProvide.wwwroot}\\files\r\n      " +
				"can_see: true\r\n      " +
				"can_list: true\r\n      " +
				"can_upload: true\r\n      " +
				"can_delete: true\r\n" +
				$"cert: {FTPServerProvide.wwwroot}\\ftp_server\\self.cert\r\n" +
				$"private_key: {FTPServerProvide.wwwroot}\\ftp_server\\self.key\r\n" +
				"https_port: 443";

			using (File.Create(configPath)) ;
			File.WriteAllText(configPath, cfg);

			AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

			serverProcess = Process.Start(serverPath);

			serverProcess.Exited += new EventHandler(ServerExit);
		}

		private void ServerExit(object? sender, EventArgs e)
		{

		}

		private void CurrentDomain_ProcessExit(object? sender, EventArgs e)
		{
			serverProcess.WaitForExit();
		}
	}

}

