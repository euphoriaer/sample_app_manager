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

		public string StorageName { get; set; } = "";

		[BsonIgnore]
		public string QRcode 
		{	get 
			{
				return NetConfig.GetURL(StorageName);
			} 
		}

		public string localPath { get; set; } = "";

		public string DefaultImage = "img/empty.png";

		public string RouteVersionName { get; set; }

		public string PiplineName { get; set; }
	}
}
