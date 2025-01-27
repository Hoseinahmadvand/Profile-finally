using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Profile.Models;
using Profile.Services.Interfaces;
using System.Text.Json;

namespace Profile.Pages.Admin.Commentmng;

public class IndexModel : PageModel
{
    private readonly ICommentService _commentService;

    public IndexModel(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public IEnumerable<Comment> Comments { get; set; }
    public async Task OnGet()
    {

        Comments = await _commentService.GetAllAsync();
    }
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var user = await _commentService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _commentService.DeleteAsync(user);
        return RedirectToPage();
    }

    public async Task<JsonResult> OnPostUploadImage([FromForm] IFormFile upload)
    {
        if (upload.Length <= 0) return null;
        var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/images",
            fileName);
        using (var stream = System.IO.File.Create(filePath))
        {
            await upload.CopyToAsync(stream);
        }
        var url = $"{"/images/"}{fileName}";
       
        return new JsonResult(url);
    }
}
