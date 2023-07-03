namespace sample_app_manager.Helper
{
	public static class NetConfig
	{
		public static string IP;

		public static string GetURL(string fileName)
		{
			var address = NetConfig.IP;
			var download = "https://" + address + $"/files/{fileName}";
			var fileURL = download;
			return fileURL;
		}
	}
}