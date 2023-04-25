using SampleAppManager.LiteDB;

namespace SampleAppManager.Data
{
	public class SeedData
	{
		public static void InitializeUser(LiteDbContext db)
		{
			var users = new UserItem[]
			{
			new UserItem()
			{
			   UserName = "Administrator",
			   IsAdmin = true,
			   Password = "666",
			   UpLoad = true,
			   isChecking = true,

			},
			new UserItem()
			{
			   UserName = "Uploader",
			   IsAdmin = false,
			   Password = "666",
			   UpLoad = true,
			   isChecking = false,
			},
			new UserItem()
			{
			   UserName = "Checker",
			   IsAdmin = false,
			   Password = "666",
			   UpLoad = false,
			   isChecking=true,
			},
			};
			var ProcessVersion = new ProcessVersion[]
			{
				new ProcessVersion()
				{
					  VersionName="1.0",
					  RouteName="10",
					  ProcessPiplines=new List<ProcessPipline>()
					  {
						   new ProcessPipline()
						   {    PiplineName="DemoAssetPipline",
								 ProcessItems=new List<ProcessItem>()
								 {
									  new ProcessItem()
									  {
											ApkName="DemoApp",
											CheckCondition=new List<CheckCondition>()
											{
												new CheckCondition()
												{
													 checkName="二次打包",
												},
												new CheckCondition()
												{
													 checkName="测试完成",
												}
											},
											 Apk=new APKItem()
											{
											Name = "DemoApp",
											Description="测试App",
											},
									  },
									  new ProcessItem()
									  {
										   ApkName="DemoApp2",
										   Apk=new APKItem()
											{
											Name = "DemoApp2",
											Description="测试App2",
											},
										   IsFinalAPK=true,
									  }
								 }
						   }
					  }
				},
				new ProcessVersion()
				{
					  VersionName="v1.6",
					  RouteName="v11",
				}
			};
			db.ProcessVersions.Add(ProcessVersion);
			db.Users.AddUser(users);
		}
	}
}