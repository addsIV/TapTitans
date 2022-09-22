using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taptitan.Models;

namespace Taptitan.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Titan.CurrentHealth = 5;
        ViewBag.Health = Titan.GetCurrentHealth();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AttackOnTitan(int attackPoint = 1)
    {
        // var currentHealth = Titan.GetCurrentHealth();
        var currentHealth = Titan.GetCurrenHealthAfterAttack(attackPoint);
        ViewBag.Health = currentHealth;
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}