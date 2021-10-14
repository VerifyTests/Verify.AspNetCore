using Microsoft.AspNetCore.Mvc.RazorPages;

public class RazorPageModel : PageModel
{
    public void OnGet()
    {
    }

    public string Value { get; set; } = null!;
}