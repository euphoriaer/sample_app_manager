using SampleAppManager.Data;

namespace SampleAppManager.Model
{
	public class ProcessVersionWithStatus
	{
		public	List<ProcessVersion> processVersion { get; set; }

		public	ProcessVersion curSelect { get; set; }
	}
}
