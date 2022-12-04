using Taptitan.Models;

namespace Taptitan.Service;

public interface ITitanService
{
    int GetCurrentHealth();
    string GetCurrentName();
    string GetCurrentElement();
    AttackResult GetAttacked(AttackDto attackDto);
    bool IsElementCounter(Element element);
}