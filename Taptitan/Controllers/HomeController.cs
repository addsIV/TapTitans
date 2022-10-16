using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taptitan.Exceptions;
using Taptitan.Models;
using Taptitan.Service;

namespace Taptitan.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPlayerService _playerService;
    private readonly ITitanService _titanService;

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
        ViewBag.TitanElement = _titanService.GetCurrentElement();
        ViewBag.MagicPoint = 10;

        return View(new AttackDto()
        {
            AttackPoint = 1,
            MagicPoint = 10,
        });
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

    public IActionResult FireBall(AttackDto attackDto)
    {
        return UseMagic(attackDto, Element.Fire);
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

    public IActionResult WaterSplash(AttackDto attackDto)
    {
        return UseMagic(attackDto, Element.Water);
    }

    public IActionResult ForestVine(AttackDto attackDto)
    {
        return UseMagic(attackDto, Element.Forest);
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
        
        ViewBag.MagicPoint = afterAttack.MagicPoint;
        ViewBag.Log1 = _titanService.Log.Count >= 1? _titanService.Log[^1]: "";
        ViewBag.Log2 = _titanService.Log.Count >= 2? _titanService.Log[^2]: "";
        ViewBag.Log3 = _titanService.Log.Count >= 3? _titanService.Log[^3]: "";
        ViewBag.Log4 = _titanService.Log.Count >= 4? _titanService.Log[^4]: "";
        ViewBag.Log5 = _titanService.Log.Count >= 5? _titanService.Log[^5]: "";
    }
}