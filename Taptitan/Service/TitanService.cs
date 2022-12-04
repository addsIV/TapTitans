using Taptitan.Models;

namespace Taptitan.Service;

public class TitanService : ITitanService
{
    private readonly IHeroService _heroService;

    private readonly ILog _log;
    public TitanService(IHeroService heroService, ILog log)
    {
        _heroService = heroService;
        _log = log;
        CurrentMaxHealth = 5;
        CurrentHealth = 5;
        CurrentName = GetNewName();
        Element = GetNewElement();
    }

    private static Element GetNewElement()
    {
        var random = new Random();
        return (Element)random.Next(1, 4);
    }
    private int CurrentMaxHealth { get; set; }

    private int CurrentHealth { get; set; }

    private string CurrentName { get; set; }

    private Element Element{ get; set; }

    private void MakeNewHealthDouble()
    {
        CurrentMaxHealth += 10;
    }

    public  int GetCurrentHealth()
    {
        return CurrentHealth;
    }
    
    public  string GetCurrentName()
    {
        return CurrentName;
    }

    public string GetCurrentElement()
    {
        return Element.ToString();
    }

    private AttackResult GetNormalAttacked()
    {
        var attackPoint = _heroService.GetAttackPoint();
        if (IsTitanWillDie(attackPoint))
        {
            MakeNewTitan();
        }
        else
        {
            CurrentHealth -= attackPoint; 
        }
        
        _log.InsertLog($"Normal Attack, damage: {attackPoint}");
        return new AttackResult()
        {
            IsSuccess = true,
        };
    }
    
    private AttackResult GetSkillAttacked(AttackDto attackDto)
    {
        var attackResult = _heroService.GetMagicAttackResult(attackDto);
        if (!attackResult.IsSuccess)
        {
            _log.InsertLog(attackResult.Reason);
            return new AttackResult()
            {
                IsSuccess = false,
                Reason = attackResult.Reason,
            };
        }

        attackResult.attackPoint = GetElementCounterResult(attackDto.Element, attackResult.attackPoint);
        if (IsTitanWillDie(attackResult.attackPoint))
        {
            MakeNewTitan();
        }
        else
        {
            CurrentHealth -= attackResult.attackPoint; 
        }

        _log.InsertLog($"Use Magic, element: {attackDto.Element}, damage: {attackResult.attackPoint}");
        return new AttackResult()
        {
            IsSuccess = true
        };
    }

    private int GetElementCounterResult(Element element, int attackPoint)
    {
        return IsElementCounter(element)
            ? attackPoint * GetElementCounterConfig().ElementCounterRatio
            : attackPoint / GetElementCounterConfig().ElementCounteredRatio;
    }

    public bool IsElementCounter(Element element)
    {
        var dif = element - Element;
        return dif is 1 or -2;
    }

    private void MakeNewTitan()
    {
        _heroService.ExpUp();
        
        MakeNewHealthDouble();
        CurrentHealth = CurrentMaxHealth;
        CurrentName = GetNewName();
        Element = GetNewElement();
    }

    public string GetNewName()
    {
        var rnd = new Random();
        var num = rnd.Next(0, 1000);
        return "Titan#" + num;
    }

    public bool IsTitanWillDie(int attackPoint)
    {
        return CurrentHealth <= attackPoint;
    }

    public AttackResult GetAttacked(AttackDto attackDto)
    {
        return attackDto.IsUseMagic ? GetSkillAttacked(attackDto) : GetNormalAttacked();
    }
    
    private static ElementCounterConfig GetElementCounterConfig()
    {
        return new ElementCounterConfig()
        {
            ElementCounterRatio = 2,
            ElementCounteredRatio = 2
        };
    }
}