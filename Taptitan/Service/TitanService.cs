using Taptitan.Models;

namespace Taptitan.Service;

public class TitanService : ITitanService
{
    public TitanService()
    {
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
    public int CurrentMaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    public string CurrentName { get; set; }
    
    public Element Element{ get; set; }

    public int GetNewHealth()
    {
        return CurrentMaxHealth * 2;
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
            GetNewTitan();
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
        if (attackDto.MagicPoint < 3)
        {
            Log.Add($"Use Magic, element: {attackDto.Element}, but magic point not enough!");
            attackDto.IsFail = true;
            return attackDto;
        }

        attackDto.MagicPoint -= 3;
        var attackPoint = attackDto.AttackPoint * 3;
        attackPoint = IsElementCounter(attackDto.Element) ? attackPoint * 2 : attackPoint;
        
        if (IsTitanWillDie(attackPoint))
        {
            GetNewTitan();
        }
        else
        {
            CurrentHealth -= attackPoint ; 
        }

        Log.Add($"Use Magic, element: {attackDto.Element}, damage: {attackPoint}");
        return attackDto;
    }

    private bool IsElementCounter(Element element)
    {
        var dif = element - Element;
        return dif is 1 or -2;
    }

    private void GetNewTitan()
    {
        CurrentHealth = GetNewHealth();
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