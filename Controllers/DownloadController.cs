using Microsoft.AspNetCore.Mvc;

namespace SampleAppManager.Controllers
{
    [Route("download")]
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
        [HttpGet("url")]
        public async Task<IActionResult> Get(string url)
        {
            try
            {
                // 解决 CentOS7 Https 下载地址出错的问题；
                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
                };


                using HttpClient client = new HttpClient(httpClientHandler);
                var stream = await client.GetStreamAsync(url);
                return File(
                    stream,
                    "application/octet-stream", // 二进制流
                    Path.GetFileName(url));
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }
    }
}
