using Microsoft.AspNetCore.Mvc;
using SampleAppManager.Data;
using SampleAppManager.LiteDB;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SampleAppManager.Controllers
{
	[Route("api/download")]
	[ApiController]
	public class DownloadController : Controller
	{
		private readonly IWebHostEnvironment env;

		public DownloadController(IWebHostEnvironment env)
		{
			this.env = env;
		}
		/// <summary>
		/// 通过 HttpClient 获取另外站点的文件流，再输出
		/// </summary>
		[HttpGet("{fileName}")]
		public async Task<IActionResult> Get(string fileName)
		{
			try
			{

				string wwwPath = env.WebRootPath;

				var path = Path.Combine(wwwPath, "files",
						$"{fileName}");
				var filePath = path;

				if (!System.IO.File.Exists(filePath))
				{
					//Todo 弹出警告，文件不存在
					return null;
				}
				using (var bytes = System.IO.File.ReadAllBytesAsync(filePath))
				{
					return File(bytes.Result, "application/octet-stream", Path.GetFileName(filePath));
				}
			}
			catch (Exception ex)
			{

			}
			return null;
		}
	}

	[ApiController]
	[Route("/api/file")]
	public class UploadController : Controller
	{
		private IWebHostEnvironment env;

		private LiteDbContext liteDbContext;
		public UploadController(IWebHostEnvironment env, LiteDbContext context)
		{
			liteDbContext=context;
			this.env = env;
		}

		[RequestSizeLimit(52428800000)]
		[HttpPost, Route("upload")]
		public  void UploadFile(IFormCollection formCollection)
		{
			List<APKItem> Apks = new List<APKItem>(formCollection.Count);
			foreach (var formFile in formCollection.Files)
			{
				var x = formFile.Name;
				var originalName = formFile.FileName;
				

				var storageFileName = Path.GetRandomFileName() + formFile.FileName;
				var filePath = Path.Combine(env.WebRootPath,"files", storageFileName);
				var folder = Path.Combine(env.WebRootPath, "files");
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
