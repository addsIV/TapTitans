namespace Taptitan.Service;

public class TitanService : ITitanService
{
    public TitanService()
    {
        CurrentMaxHealth = 5;
        CurrentHealth = 5;
        CurrentName = GetNewName();
    }

    public int CurrentMaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    public string CurrentName { get; set; }

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

    public  void GetAttacked(int attackPoint)
    {
        if (IsTitanWillDie(attackPoint))
        {
            CurrentHealth = GetNewHealth();
            CurrentName = GetNewName();
        }
        else
        {
            CurrentHealth -= attackPoint; 
        }
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
}