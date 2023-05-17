using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace SampleAppManager.FTPServer
{
	public class FTPServerProvide
	{
		protected NavigationManager NavigationManager { get; }
		private string url;
		private static FTPServer FTPServer;
		public FTPServerProvide(NavigationManager navigationManager)
		{
			NavigationManager = navigationManager;
			url = NavigationManager.BaseUri;
			if (FTPServer==null)
			{
				FTPServer=new FTPServer(url);
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

		public FTPServer(string url)
		{
			this.url = url;
		}
	}

}

