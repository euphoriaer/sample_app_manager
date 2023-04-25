using AntDesign;
using System.ComponentModel.DataAnnotations;

namespace SampleAppManager.Data
{
	public class APKItem
	{
		
		public string Name { get; set; } = "DefaultName";

		public string Description { get; set; } = "";

		public string DownLoadURL { get; set; } = "";

		public string QRcode { get; set; } = "";

		public string localPath { get; set; } = "";

		public string DefaultImage = "img/empty.png";
	}
}
