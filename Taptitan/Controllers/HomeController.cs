using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taptitan.Exceptions;
using Taptitan.Models;
using Taptitan.Service;

namespace Taptitan.Controllers;

public class HomeController : Controller
{
    private readonly ILog _log;
    private readonly ITitanService _titanService;
    private readonly IHeroService _heroService;

    public HomeController(ILog log, ITitanService titanService, IHeroService heroService)
    {
        _log = log;
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
        InitializeHeroState(attackPoint, magicPoint, level, exp);
        return View();
    }

    private void InitializeTitanState()
    {
        ViewBag.TitanHealth = _titanService.GetCurrentHealth();
        ViewBag.TitanName = _titanService.GetCurrentName();
        ViewBag.TitanElement = _titanService.GetCurrentElement();
    }

    private void InitializeHeroState(int attackPoint, int magicPoint, int level, int exp)
    {
        ViewBag.AttackPoint = attackPoint;
        ViewBag.MagicPoint = magicPoint;
        ViewBag.Level = level;
        ViewBag.Exp = exp;
        ViewBag.MaxExp = level;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Attack(AttackDto attackDto)
    {
        var afterAttack = _titanService.GetAttacked(attackDto);
        
        if (!afterAttack.IsSuccess)
        {
            return BadRequest(afterAttack.Reason);
        }
        
        return Ok();
    }

    [HttpGet]
    public TitanStatusDto GetTitanStatus()
    {
        return new TitanStatusDto
        {
            Health = _titanService.GetCurrentHealth(),
            Name = _titanService.GetCurrentName(),
            Element = _titanService.GetCurrentElement()
        };
    }
    
    [HttpGet]
    public HeroStatusDto GetHeroStatus()
    {
        return new HeroStatusDto
        {
            Level = _heroService.GetLevel(),
            Exp = _heroService.GetExp(),
            MaxExp = _heroService.GetMaxExp(),
            AttackPoint = _heroService.GetAttackPoint(),
            MagicPoint = _heroService.GetMagicPoint()
        };
    }

    [HttpGet]
    public List<string> GetLog()
    {
        var log = _log.GetLog();
        return log;
    }
}