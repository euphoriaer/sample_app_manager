using System.Drawing;

namespace SampleAppManager.Model
{

	public class Authentication
	{
		public bool isUpload { get; set; } = false;

		public bool isAdmin { get; set; } = false;
		public bool isNewVersion { get; set; } = false;
		public bool isChecking { get; set; } = false;
	}
}
