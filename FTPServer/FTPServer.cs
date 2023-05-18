using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace SampleAppManager.FTPServer
{
	public class FTPServerProvide
	{
		protected NavigationManager NavigationManager { get; }
		private string url;
		private static FTPServer FTPServer;
		public FTPServerProvide(NavigationManager navigationManager, IWebHostEnvironment env)
		{
			NavigationManager = navigationManager;
			url = NavigationManager.BaseUri;
			var serverConfig = Path.Combine(env.ContentRootPath, "config.yaml");
			if (FTPServer == null)
			{
				var folder = Path.Combine(env.WebRootPath, "files");
				var serverPath = Path.Combine(env.WebRootPath, "ftp_server", "hfs-windows", "hfs.exe");
				FTPServer = new FTPServer(url, folder, serverPath, serverConfig);
			}
		}

		public void UrlTest()
		{
			Debug.WriteLine(FTPServer.url);
		}
	}



	public class FTPServer
	{
		public string url;

		public Process serverProcess;

		public FTPServer(string url, string savePath, string serverPath, string configPath)
		{

			if (File.Exists(configPath))
			{
				File.Delete(configPath);
			}
			string cfg = $"vfs:\r\n  children:\r\n    - source: {savePath}\n      can_see: true\r\n      can_list: true\r\n      can_upload: true\r\n      can_delete: true";
			using(File.Create(configPath));
			File.WriteAllText(configPath, cfg);

			AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

			serverProcess = Process.Start(serverPath, "help");
			//var configPath = Path.Combine(Path.GetDirectoryName(serverPath), "config.yaml");
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

