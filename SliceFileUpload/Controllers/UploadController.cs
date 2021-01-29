using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SliceFileUpload.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SliceFileUpload.Controllers
{
    /*
     * 
     * 一、常见的文件存储选项有：
        1、数据库
            1.1.对于小型文件上传，数据库通常快于物理存储（文件系统或网络共享）选项。
            1.2.相对于物理存储选项，数据库通常更为便利，因为检索数据库记录来获取用户数据可同时提供文件内容（如头像图像）。
            1.3.相对于使用数据存储服务，数据库的成本可能更低。
        2、物理存储（文件系统或网络共享）
            2.1.对于大型文件上传：
                2.1.1数据库限制可能会限制上传的大小。
            2.2相对于数据库存储，物理存储通常成本更高。
            2.3相对于使用数据存储服务，物理存储的成本可能更低。
            2.4应用的进程必须具有存储位置的读写权限。 切勿授予执行权限。
        3、数据存储服务（例如，Azure Blob 存储）
            3.1服务通常通过本地解决方案提供提升的可伸缩性和复原能力，而它们往往受单一故障点的影响。
            3.2在大型存储基础结构方案中，服务的成本可能更低。


        二、安全注意事项:
        1、如何降低攻击可能性的安全措施
            1.1将文件上传到专用文件上传区域，最好是非系统驱动器。 使用专用位置便于对上传的文件实施安全限制。 禁用对文件上传位置的执行权限。†
            1.2请勿将上传的文件保存在与应用相同的目录树中。†
            1.3使用应用确定的安全的文件名。 请勿使用用户提供的文件名或上载文件的不受信任的文件名。 † 显示时，HTML 对不受信任的文件名进行编码。 例如，记录文件名或在 UI 中显示 (Razor 会自动对输出) 进行 HTML 编码。
            1.4仅允许应用设计规范的已批准文件扩展名。†
            1.5验证是否在服务器上执行了客户端检查。 † 客户端检查很容易规避。
            1.6检查已上传文件的大小。 设置大小上限以防止上传大型文件。†
            1.7文件不应该被具有相同名称的上传文件覆盖时，先在数据库或物理存储上检查文件名，然后再上传文件。
            1.8先对上传的内容运行病毒/恶意软件扫描程序，然后再存储文件。
     */
    public class UploadController : ControllerBase
    {
        public UploadController(IOptions<ConfigSettingModel> configSettings, ILogger<UploadController> logger) : base(configSettings, logger)
        { }

        /*
         * 单文件接口
         */
        public async Task<IActionResult> File(IFormFile file) {
            return Ok();
        }

        /*
         * 多文件接口
         */
        public async Task<IActionResult> Files(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok();
        }
    }
}
