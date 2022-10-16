namespace Taptitan.Service;

public interface ITitanService
{
    int CurrentMaxHealth { get; set; }
    int CurrentHealth { get; set; }
    string CurrentName { get; set; }
    int GetNewHealth();
    int GetCurrentHealth();
    string GetCurrentName();
    void GetAttacked(int attackPoint);
    string GetNewName();
    bool IsTitanWillDie(int attackPoint);
}