using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
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
		protected NavigationManager NavigationManager;
		public UploadController(NavigationManager navigationManager, IWebHostEnvironment env)
		{
			NavigationManager=navigationManager;
			this.env = env;
		}

		[HttpPost, Route("upload")]
		public void UploadFile(IFormCollection formCollection)
		{

			foreach (var item in formCollection.Files)
			{
				
				Debug.WriteLine(item.Name);
				Debug.WriteLine(item.FileName);
				//https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-7.0
				//System.Uri uri = new Uri(NavigationManager.BaseUri, false);
				//var address = uri.Host;
				//var uploadUrl = "http://" + address + "/files/";

				//var client = new HttpClient();
				//var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.2.109/files/");
				//var content = new MultipartFormDataContent();

				//FileStream fi=new FileStream("D:/AndroidPlayer.zip",FileMode.Open);

				////var stream = item.OpenReadStream().CopyToAsync(memoryStream,(int)item.OpenReadStream().Length);

				//content.Add(new StreamContent(item.OpenReadStream()));
				//request.Content = content;
				//var response = await client.SendAsync(request);
				//response.EnsureSuccessStatusCode();
				//Console.WriteLine(await response.Content.ReadAsStringAsync());

				var options = new RestClientOptions("http://10.0.2.109")
				{
					MaxTimeout = -1,
				};
				var client = new RestClient(options);
				var request = new RestRequest("/files/", Method.Post);
				request.AlwaysMultipartFormData = true;
				request.AddFile("", () => { return item.OpenReadStream(); },item.FileName);
				RestResponse response = client.Execute(request);
				Console.WriteLine(response.IsSuccessful);
				Debug.WriteLine(response.IsSuccessful);
			}

		}
	}
}
