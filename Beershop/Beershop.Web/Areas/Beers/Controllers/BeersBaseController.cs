using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerShop.Web.Areas.Beers.Controllers
{
    [Area("Beers")]
    [Authorize]
    public abstract class BeersBaseController : Controller
    {
    }
}
