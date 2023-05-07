
using LiteDB;

namespace SampleAppManager.Data
{
	public class UserItem
	{
		[BsonId]
		public string UserName { get; set; }
	
		public string Password { get; set; }
	
		public bool IsAdmin { get; set; } = false;
	
		public bool UpLoad { get; set; } = true;

		public bool isChecking { get; set; } = false;

		public string Role { get; set; } = "Administrator";
	}

}
