using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taptitan.Exceptions;
using Taptitan.Models;
using Taptitan.Service;

namespace Taptitan.Controllers;

public class HomeController : Controller
{
    private readonly IHeroService _heroService;
    private readonly ILogger<HomeController> _logger;
    private readonly ITitanService _titanService;

    public HomeController(ILogger<HomeController> logger, ITitanService titanService, IHeroService heroService)
    {
        _logger = logger;
        _titanService = titanService;
        _heroService = heroService;
    }

    public IActionResult Index()
    {
        InitializeTitanState();

        const int attackPoint = 1;
        const int magicPoint = 10;
        const int level = 1;
        const int exp = 0;
        var attackDto = InitializeHeroState(attackPoint, magicPoint, level, exp);
        return View(attackDto);
    }

    private void InitializeTitanState()
    {
        ViewBag.TitanHealth = _titanService.GetCurrentHealth();
        ViewBag.TitanName = _titanService.GetCurrentName();
        ViewBag.TitanElement = _titanService.GetCurrentElement();
    }

    private AttackDto InitializeHeroState(int attackPoint, int magicPoint, int level, int exp)
    {
        ViewBag.MagicPoint = magicPoint;
        ViewBag.Level = level;
        ViewBag.Exp = exp;
        ViewBag.MaxExp = level;

        return new AttackDto()
        {
            AttackPoint = attackPoint,
            MagicPoint = magicPoint
        };
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Attack(AttackDto attackDto)
    {
        var afterAttack = _titanService.GetAttacked(attackDto);
        PrepareTitanStateForDisplay(afterAttack);
        return View("Index", afterAttack);
    }

    [HttpPost]
    public IActionResult FireBall(AttackDto attackDto)
    {
        return UseMagic(attackDto, Element.Fire);
    }

    [HttpPost]
    public IActionResult WaterSplash(AttackDto attackDto)
    {
        return UseMagic(attackDto, Element.Water);
    }

    [HttpPost]
    public IActionResult ForestVine(AttackDto attackDto)
    {
        return UseMagic(attackDto, Element.Forest);
    }

    private IActionResult UseMagic(AttackDto attackDto, Element element)
    {
        attackDto.IsUseMagic = true;
        attackDto.Element = element;
        var afterAttack = _titanService.GetAttacked(attackDto);

        if (afterAttack.IsFail)
        {
            PrepareForAlertMessage("Magic point not enough!");
        }

        PrepareTitanStateForDisplay(afterAttack);
        return View("Index", afterAttack);
    }

    private void PrepareForAlertMessage(string magicPointNotEnough)
    {
        ViewBag.AlertMessage = magicPointNotEnough;
    }

    private void PrepareTitanStateForDisplay(AttackDto afterAttack)
    {
        ViewBag.TitanHealth = _titanService.GetCurrentHealth();
        ViewBag.TitanName = _titanService.GetCurrentName();
        ViewBag.TitanElement = _titanService.GetCurrentElement();
        ViewBag.Level = _heroService.GetCurrentLevel();
        ViewBag.Exp = _heroService.GetCurrentExp();
        ViewBag.MaxExp = _heroService.GetCurrentMaxExp();

        ViewBag.MagicPoint = afterAttack.MagicPoint;
        ViewBag.Log1 = _titanService.Log.Count >= 1 ? _titanService.Log[^1] : "";
        ViewBag.Log2 = _titanService.Log.Count >= 2 ? _titanService.Log[^2] : "";
        ViewBag.Log3 = _titanService.Log.Count >= 3 ? _titanService.Log[^3] : "";
        ViewBag.Log4 = _titanService.Log.Count >= 4 ? _titanService.Log[^4] : "";
        ViewBag.Log5 = _titanService.Log.Count >= 5 ? _titanService.Log[^5] : "";
    }
}