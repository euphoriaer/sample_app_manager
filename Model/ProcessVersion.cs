using LiteDB;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleAppManager.Data
{
	public class ProcessVersion
	{
		[BsonId]
		public string RouteName { get; set; }
		public string VersionName { get; set; }

		public List<ProcessPipline> ProcessPiplines { get; set; } = new List<ProcessPipline>();

		public List<APKItem> GetAPKItems()
		{
			List<APKItem> items = new List<APKItem>();
			for (int i = 0; i < ProcessPiplines.Count; i++)
			{
				for (int j = 0; j < ProcessPiplines[i].ProcessItems.Count; j++)
				{
					var app = ProcessPiplines[i].ProcessItems[j].Apk;
					items.Add(app);
				}
			}
			return items;
		}
	}

	public class ProcessPipline
	{

		public string PiplineName { get; set; } = "Default";

		public List<ProcessItem> ProcessItems { get; set; } = new List<ProcessItem>();
	}


	public class ProcessItem
	{

		public string ApkName { get; set; } = "DefaultApkName";

		public APKItem Apk { get; set; } = new APKItem();

		/// <summary>
		/// 可能没有，作为app管线最后的包
		/// </summary>
		public List<CheckCondition> CheckCondition { get; set; } = new List<CheckCondition>();

		public bool Finish { get; set; } = false;

		public bool IsFinalAPK { get; set; } = false;
	}


	public class CheckCondition
	{
		public string checkName { get; set; } = string.Empty;
		public bool checkState { get; set; } = false;
	}
}