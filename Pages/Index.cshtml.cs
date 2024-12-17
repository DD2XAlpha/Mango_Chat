using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mango_Chat.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    

    public JsonResult OnGetFetchData(int id)
    {
        var data = new
        {
            Message = $"You passed ID: {id}",
            Timestamp = DateTime.Now
        };
        return new JsonResult(data);
    }

}
