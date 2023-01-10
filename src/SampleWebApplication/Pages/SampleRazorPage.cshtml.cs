using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SampleWebApplication;

public class SampleRazorPage : PageModel
{
    public IActionResult OnGet() =>
        Page();
}