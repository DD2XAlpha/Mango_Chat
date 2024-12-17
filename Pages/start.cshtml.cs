using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class startModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetModel(string modelType)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "json", $"{modelType}.json");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            string jsonString = System.IO.File.ReadAllText(filePath);
            return new JsonResult(jsonString);
        }
    }
}
