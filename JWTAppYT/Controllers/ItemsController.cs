using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAppYT.Controllers
{
    [Authorize]
    [ApiController]
    public class ItemsController : Controller
    {
        public List<string> colorList = new () { "blue", "red", "green", "yellow", "pink" };

        [HttpGet("GetColorList")]
        public List<string> GetColorList()
        {
            return colorList;
        }
    }
}
