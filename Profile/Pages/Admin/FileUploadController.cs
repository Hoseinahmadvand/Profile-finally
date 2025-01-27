using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Profile.Pages.Admin;

[Route("admin/api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public FileUploadController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload()
    {
        var file = Request.Form.Files[0];
        if (file != null && file.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // بازگشت لینک فایل آپلود شده
            return Ok(new { uploaded = true, url = $"{Request.Scheme}://{Request.Host}/uploads/{file.FileName}" });
       

        }

        return BadRequest(new { uploaded = false, error = new { message = "خطا در آپلود فایل." } });
    }
}

