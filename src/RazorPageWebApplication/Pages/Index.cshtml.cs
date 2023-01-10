using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageWebApplication.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet() =>
        Page();
}