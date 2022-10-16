using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taptitan.Exceptions;
using Taptitan.Models;
using Taptitan.Service;

namespace Taptitan.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITitanService _titanService;
    private readonly IPlayerService _playerService;

    public HomeController(ILogger<HomeController> logger, ITitanService titanService, IPlayerService playerService)
    {
        _logger = logger;
        _titanService = titanService;
        _playerService = playerService;
    }

    public IActionResult Index()
    {
        ViewBag.TitanHealth = _titanService.GetCurrentHealth();
        ViewBag.TitanName = _titanService.GetCurrentName();
        ViewBag.MagicPoint = 10;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Attack(int attackPoint)
    {
        _titanService.GetAttacked(attackPoint);
        ViewBag.TitanHealth = _titanService.GetCurrentHealth();
        ViewBag.TitanName = _titanService.GetCurrentName();
        ViewBag.MagicPoint = _playerService.GetCurrentMagicPoint();
        return View("Index");
    }
    
    [HttpPost]
    public IActionResult FireBall(int attackPoint)
    {
        try
        {
            _playerService.UseFireBall(3);
            return Attack(attackPoint * 3);
        }
        catch (MagicPointNotEnoughException e)
        {
            ViewBag.MagicPointNotEnoughMessage = e.Message;
            Console.WriteLine(e);
            return Attack(0);
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}