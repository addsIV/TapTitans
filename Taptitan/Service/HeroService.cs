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
    }
    private int Level { get; set; }
    private int Exp { get; set; }
    private int MaxExp { get; set; }
    private int AttackPoint { get; set; }
    private int MagicPoint { get; set; }
    private int MaxMagicPoint { get; set; }
    public int GetCurrentLevel()
    {
        return Level;
    }

    public int GetCurrentExp()
    {
        return Exp;
    }

    public int GetCurrentMaxExp()
    {
        return MaxExp;
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
        
        if (attackDto.MagicPoint < magicConfig.MagicCost)
        {
            return new AttackResult()
            {
                IsSuccess = false,
                Reason = $"Use Magic, element: {attackDto.Element}, but magic point not enough!",
                attackPoint = attackDto.AttackPoint
            };
        }

        attackDto.MagicPoint -= magicConfig.MagicCost;
        
        var attackPoint = attackDto.AttackPoint * magicConfig.MagicRatio;

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