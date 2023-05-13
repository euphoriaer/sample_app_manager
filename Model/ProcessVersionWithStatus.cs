using SampleAppManager.Data;

namespace SampleAppManager.Model
{
	public class ProcessVersionWithStatus
	{
		public	List<ProcessVersion> processVersion { get; set; }=new List<ProcessVersion>();

		public	ProcessVersion curSelect { get; set; }=new ProcessVersion();
	}
}
