using Taptitan.Models;

namespace Taptitan.Service;

public interface ITitanService
{
    List<string> Log { get; set; }
    int GetCurrentHealth();
    string GetCurrentName();
    string GetCurrentElement();
    AttackDto GetAttacked(AttackDto attackDto);
    bool IsElementCounter(Element element);
}