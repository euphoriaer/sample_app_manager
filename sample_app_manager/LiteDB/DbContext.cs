using LiteDB;
using sample_app_manager.Helper;
using SampleAppManager.Data;
using static SampleAppManager.LiteDB.LiteDbContext;


namespace SampleAppManager.LiteDB
{
	public class LiteDbContext
	{
		private string _dbConnectionString;
		public User Users;
		public ProcessVersionDb ProcessVersions;
		public AppData Apps;
		public LiteDbContext(string dbPath)
		{
			_dbConnectionString = dbPath;
			Users = new User(dbPath);
			ProcessVersions = new ProcessVersionDb(dbPath);
			Apps = new AppData(dbPath);
		}

		public LiteDatabase GetDB()
		{
			return new LiteDatabase(_dbConnectionString);
		}

		public class ProcessVersionDb
		{
			private string dbPath;

			public ProcessVersionDb(string dbPath)
			{
				this.dbPath = dbPath;
			}

			public LiteDatabase GetDB()
			{
				return new LiteDatabase(dbPath);
			}

			public List<ProcessVersion> GetProcessVersion()
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<ProcessVersion>();
					var list = col.FindAll().OrderBy(x=>x.CustomerId.CreationTime).ToList();
					return list;
				}
			}

			public List<ProcessVersion> GetProcessVersion(string RouteName)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<ProcessVersion>();
					var res= col.Query()
					.Where(x => x.RouteName == RouteName);

                    return res.ToList();
				}
			}

			public bool Update(ProcessVersion item)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<ProcessVersion>();

					var apks= db.GetCollection<APKItem>();
					var upDateApk= item.GetAPKItems();
					apks.Upsert(upDateApk);
                    return col.Update(item);
				}
			}

			public void Add(params ProcessVersion[] user)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<ProcessVersion>();
					col.Insert(user);
				}
			}

			public void Delete(ProcessVersion version)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<ProcessVersion>();

					var apks = db.GetCollection<APKItem>();
					var upDateApk = version.GetAPKItems();
					for (int i = 0; i < upDateApk.Count; i++)
					{
						if (upDateApk[i].CustomerId == null)
						{
							continue;
						}
						apks.Delete(upDateApk[i].CustomerId);
					}

					bool isOk=col.Delete(version.CustomerId);
				}
			}
		}

		public class User
		{
			private string dbPath;

			public LiteDatabase GetDB()
			{
				return new LiteDatabase(dbPath);
			}

			public User(string dbPath)
			{
				this.dbPath = dbPath;
			}

			public List<UserItem> GetUser()
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<UserItem>();
					var list = col.FindAll().ToList();
					return list;
				}
			}
			public List<UserItem> GetUser(string userName)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<UserItem>();
					var list = col.Query().Where(x=>x.UserName == userName).ToList();
					return list;
				}
			}

			public bool UpdateUser(UserItem user)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<UserItem>();
					return col.Update(user);
				}
			}

			public void AddUser(params UserItem[] user)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<UserItem>();
					col.Insert(user);
				}
			}

			public void DeleteUser(string userName)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<UserItem>();
					col.Delete(userName);
				}
			}
		}

		public class AppData
		{
			private string dbPath;

			private LiteDatabase _db;
			public LiteDatabase GetDB()
			{
				if (_db == null)
				{
					return new LiteDatabase(dbPath);
				}
				else
				{
					return _db;
				}
				
			}

			public AppData(string dbPath)
			{
				this.dbPath = dbPath;
			}

			public List<APKItem> Get()
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<APKItem>();
					var list = col.FindAll().ToList();
					for (int i = 0; i < list.Count; i++)
					{
						var curApp = list[i];
						if (curApp.DownLoadURL==null)
						{
							continue;
						}
						curApp.DownLoadURL = GetNewDownloadURL(curApp.DownLoadURL);
					}
					return list;
				}
			}
			public string GetNewDownloadURL(string DownLoadURL)
			{
				var index = DownLoadURL.LastIndexOf("/");
				var name = DownLoadURL.Split("/", index).Last();

				var address = NetConfig.IP;
				var download = "https://" + address + $"/files/{name}";
				return download;
			}
			public List<APKItem> Get(ObjectId apkID)
			{
				var id = ObjectId.NewObjectId();
				using (var db = GetDB())
				{
					var col = db.GetCollection<APKItem>();
					var list = col.Query().Where(x => x.CustomerId == apkID).ToList();
					for (int i = 0; i < list.Count; i++)
					{
						var curApp = list[i];
						curApp.DownLoadURL = GetNewDownloadURL(curApp.DownLoadURL);
					}
					return list;
				}
			}

			public bool Update(APKItem item)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<APKItem>();
					return col.Update(item);
				}
			}

			public void Add(params APKItem[] item)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<APKItem>();
					col.Insert(item);
				}
			}

			public void Delete(ObjectId objectId)
			{
				using (var db = GetDB())
				{
					var col = db.GetCollection<APKItem>();
					col.Delete(objectId);
				}
			}
		}
	}
}