using Microsoft.AspNetCore.Mvc;

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
                using (var bytes =System.IO.File.ReadAllBytesAsync(filePath))
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
}
