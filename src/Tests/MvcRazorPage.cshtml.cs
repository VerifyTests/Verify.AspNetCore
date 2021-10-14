using Microsoft.AspNetCore.Mvc.RazorPages;

public class MvcRazorPageModel : PageModel
{
    public void OnGet()
    {
    }

    public string Value { get; set; } = null!;
}