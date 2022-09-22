namespace Taptitan.Models;

public static class Titan
{
    static Titan()
    {
        CurrentMaxHealth = 5;
        CurrentHealth = 5;
    }


    private static int CurrentMaxHealth { get; set; }

    private static int CurrentHealth { get; set; }

    private static void ResetHealth()
    {
        CurrentMaxHealth *= 2;
        CurrentHealth = CurrentMaxHealth;
    }

    public static int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public static int GetCurrenHealthAfterAttack(int attackPoint)
    {
        if (CurrentHealth >= attackPoint)
        {
            CurrentHealth -= attackPoint;
        }
        else
        {
            ResetHealth();
        }

        return CurrentHealth;
    }
}