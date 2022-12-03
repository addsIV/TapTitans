using Taptitan.Models;

namespace Taptitan.Service;

public interface IHeroService
{
    int GetCurrentLevel();
    int GetCurrentExp();
    int GetCurrentMaxExp();
    void ExpUp();
    AttackResult GetMagicAttackResult(AttackDto attackDto);
}