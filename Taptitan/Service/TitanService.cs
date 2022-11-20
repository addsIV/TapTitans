using Taptitan.Models;

namespace Taptitan.Service;

public class TitanService : ITitanService
{
    private readonly IHeroService _heroService;
    public TitanService(IHeroService heroService)
    {
        _heroService = heroService;
        CurrentMaxHealth = 5;
        CurrentHealth = 5;
        CurrentName = GetNewName();
        Element = GetNewElement();
    }

    private static Element GetNewElement()
    {
        var random = new Random();
        return (Element)random.Next(1, 3);
    }

    public List<string> Log { get; set; } = new List<string>();
    private int CurrentMaxHealth { get; set; }

    private int CurrentHealth { get; set; }

    private string CurrentName { get; set; }

    private Element Element{ get; set; }

    private void MakeNewHealthDouble()
    {
        CurrentMaxHealth *= 2;
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

    private AttackDto GetNormalAttacked(AttackDto attackDto)
    {
        if (IsTitanWillDie(attackDto.AttackPoint))
        {
            MakeNewTitan();
        }
        else
        {
            CurrentHealth -= attackDto.AttackPoint; 
        }
        
        Log.Add($"Normal Attack, damage: {attackDto.AttackPoint}");
        return attackDto;
    }
    
    private AttackDto GetSkillAttacked(AttackDto attackDto)
    {
        var attackResult = _heroService.GetMagicAttackResult(attackDto);
        if (!attackResult.IsSuccess)
        {
            Log.Add(attackResult.Reason);
            return attackDto;
        }
        
        if (IsTitanWillDie(attackResult.attackPoint))
        {
            MakeNewTitan();
        }
        else
        {
            CurrentHealth -= attackResult.attackPoint; 
        }

        Log.Add($"Use Magic, element: {attackDto.Element}, damage: {attackResult.attackPoint}");
        return attackDto;
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

    public AttackDto GetAttacked(AttackDto attackDto)
    {
        return attackDto.IsUseMagic ? GetSkillAttacked(attackDto) : GetNormalAttacked(attackDto);
    }
}