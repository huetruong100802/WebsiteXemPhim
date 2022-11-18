using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieWebsite.Filters;

namespace MovieWebsite.Controllers
{
    [GlobalFilter]

    [Authorize(Policy = "AdminPolicy")]
    public class BaseController : Controller
    {
    }
}
