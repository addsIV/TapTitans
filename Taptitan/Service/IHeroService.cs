using Taptitan.Models;

namespace Taptitan.Service;

public interface IHeroService
{
    int GetLevel();
    int GetExp();
    int GetMaxExp();
    void ExpUp();
    AttackResult GetMagicAttackResult(AttackDto attackDto);
    int GetMagicPoint();
    int GetAttackPoint();
}