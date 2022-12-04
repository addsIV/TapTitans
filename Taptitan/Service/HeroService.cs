using Taptitan.Exceptions;
using Taptitan.Models;

namespace Taptitan.Service;

public  class HeroService : IHeroService
{
    public HeroService()
    {
        Level = 1;
        Exp = 0;
        MaxExp = Level;
        MagicPoint = 10;
        MaxMagicPoint = 10;
    }
    private int Level { get; set; }
    private int Exp { get; set; }
    private int MaxExp { get; set; }

    private int AttackPoint => Level / 2 == 0? 1: Level / 2;

    private int MagicPoint { get; set; }
    private int MaxMagicPoint { get; set; }
    public int GetLevel()
    {
        return Level;
    }

    public int GetExp()
    {
        return Exp;
    }

    public int GetMaxExp()
    {
        return MaxExp;
    }
    public int GetAttackPoint()
    {
        return AttackPoint;
    }
    public int GetMagicPoint()
    {
        return MagicPoint;
    }

    public void ExpUp()
    {
        Exp++;
        if (Exp>=MaxExp)
        {
            LevelUp();
        }
    }

    public AttackResult GetMagicAttackResult(AttackDto attackDto)
    {
        var magicConfig = GetMagicConfig();
        
        if (MagicPoint < magicConfig.MagicCost)
        {
            return new AttackResult()
            {
                IsSuccess = false,
                Reason = $"Use Magic, element: {attackDto.Element}, but magic point not enough!",
                attackPoint = AttackPoint
            };
        }

        MagicPoint -= magicConfig.MagicCost;
        
        var attackPoint = AttackPoint * magicConfig.MagicRatio;

        return new AttackResult
        {
            IsSuccess = true,
            Reason = "",
            attackPoint = attackPoint
        };
    }

    private void LevelUp()
    {
        Exp -= MaxExp;
        Level++;
        MaxExp = Level;
        ResetMagicPoint();
    }

    private void ResetMagicPoint()
    {
        MagicPoint = MaxMagicPoint;
    }

    private static MagicConfig GetMagicConfig()
    {
        return new MagicConfig()
        {
            MagicCost = 1,
            MagicRatio = 4,
        };
    }
}