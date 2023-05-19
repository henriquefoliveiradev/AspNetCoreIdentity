using Microsoft.AspNetCore.Mvc;
using KissLog;

namespace AspNetCoreIdentity.Controllers
{
    public class TesteController : Controller
    {
        private readonly IKLogger _logger;

        public TesteController(IKLogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Trace("Esse erro aconteceu");

            return View();
        }
    }
}
