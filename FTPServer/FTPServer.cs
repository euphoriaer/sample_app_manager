﻿using Microsoft.AspNetCore.Components;
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
				var serverPath = Path.Combine(env.WebRootPath, "ftp_server", "hfs.exe");
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
			string cfg = $"vfs:\r\n  children:\r\n    " +
				$"- source: {savePath}\n      " +
				$"can_see: true\r\n      " +
				$"can_list: true\r\n      " +
				$"can_upload: true\r\n      " +
				$"can_delete: true";
			using(File.Create(configPath));
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

