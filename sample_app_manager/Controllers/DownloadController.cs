using Microsoft.AspNetCore.Mvc;
using SampleAppManager.Data;
using SampleAppManager.FTPServer;
using SampleAppManager.LiteDB;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SampleAppManager.Controllers
{
	[ApiController]
	[Route("/api/file")]
	public class UploadController : Controller
	{
		private IWebHostEnvironment env;
		private const int MaxFileSize=int.MaxValue;

		private LiteDbContext liteDbContext;
		public UploadController(IWebHostEnvironment env, LiteDbContext context)
		{
			liteDbContext=context;
			this.env = env;
		}

		[RequestSizeLimit(MaxFileSize)]
		[HttpPost, Route("upload")]
		public  void UploadFile(IFormCollection formCollection)
		{
			List<APKItem> Apks = new List<APKItem>(formCollection.Count);
			foreach (var formFile in formCollection.Files)
			{
				var x = formFile.Name;
				var originalName = formFile.FileName;
				

				var storageFileName = Path.GetRandomFileName() + formFile.FileName;
				var filePath = Path.Combine(FTPServerProvide.envWebRootPath,"files", storageFileName);
				var folder = Path.Combine(FTPServerProvide.envWebRootPath, "files");
				if (!Directory.Exists(folder))
				{
					Directory.CreateDirectory(folder);

				}

				using (var stream = System.IO.File.Create(filePath))
				{
					formFile.CopyTo(stream);
				}
				APKItem item = new APKItem();

				var address = HttpContext.Request.Host.Host;
				var downloadURL = "https://" + address + $"/files/{storageFileName}";
				item.localPath = filePath;
				item.DownLoadURL = downloadURL;
				item.QRcode = downloadURL;
				item.Name = originalName;
				Apks.Add(item);
			}

			liteDbContext.Apps.Add(Apks.ToArray());

		}
	}
}
