using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Appapi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UploadFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public UploadFileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("postUploadFiles")]
        public async Task<IActionResult> FilesUpload(List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return Content("file not selected");

                string fileNameSaving = "noimage.png";
                long size = files.Sum(f => f.Length);
                var fileList = new List<object>();
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        var folderName = Path.Combine("wwwroot", "uploads");
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        fileNameSaving = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                        var dbPath = Path.Combine(folderName, fileNameSaving);
                        fileList.Add(new
                        {
                            // paht = "uploads/" + fileNameSaving,
                            paht = dbPath,
                            name = formFile.FileName
                        });
                        var fileNameWithPath = Path.Combine(filePath, fileNameSaving);
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
                // process uploaded files
                // Don't rely on or trust the FileName property without validation.
                return Ok(new { count = files.Count, size, fileList });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


    }
}