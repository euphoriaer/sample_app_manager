using AntDesign;
using LiteDB;
using sample_app_manager.Helper;
using System.ComponentModel.DataAnnotations;

namespace SampleAppManager.Data
{
	public class APKItem
	{
		[BsonId]
		public ObjectId CustomerId { get; set; }

		public string Name { get; set; } = "DefaultName";

		public string Description { get; set; } = "";

		public string DownLoadURL { get; set; } = "";
		[BsonIgnore]
		public string NewDownLoadURL {
			get
			{
				var index = DownLoadURL.LastIndexOf("/");
				var name = DownLoadURL.Split("/", index).Last();

				var address = NetConfig.IP;
				var download = "https://" + address + $"/files/{name}";
				return download;
			}
		}

		public string QRcode { get; set; } = "";

		public string localPath { get; set; } = "";

		public string DefaultImage = "img/empty.png";

		public string RouteVersionName { get; set; }

		public string PiplineName { get; set; }
	}
}
