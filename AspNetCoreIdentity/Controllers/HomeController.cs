using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreIdentity.Extensions;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult Secret()
        {
            return View();
        }


        [ClaimsAuthorize("Permissao", "PodeEscrsever")]
        public IActionResult SecretClaimEscrever()
        {
            return View("Secret");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}