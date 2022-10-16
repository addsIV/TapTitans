using Taptitan.Models;

namespace Taptitan.Service;

public interface ITitanService
{
    List<string> Log { get; set; }
    int CurrentMaxHealth { get; set; }
    int CurrentHealth { get; set; }
    string CurrentName { get; set; }
    Element Element{ get; set; }
    int GetNewHealth();
    int GetCurrentHealth();
    string GetCurrentName();
    string GetCurrentElement();
    string GetNewName();
    bool IsTitanWillDie(int attackPoint);
    AttackDto GetAttacked(AttackDto attackDto);
}