using System.Diagnostics;


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
			$"- source: {savePath}\r\n      " +
			"can_see: true\r\n      " +
			"can_list: true\r\n      " +
			"can_upload: true\r\n      " +
			"can_delete: true\r\n";

		using (File.Create(configPath));

		File.WriteAllText(configPath, cfg);


		serverProcess = Process.Start(serverPath);

		AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

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



