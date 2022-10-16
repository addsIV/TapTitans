using Taptitan.Exceptions;

namespace Taptitan.Service;

public  class PlayerService : IPlayerService
{
    public PlayerService()
    {
        AttackPoint = 1;
        MagicPoint = 10;
    }

    public int AttackPoint { get; set; }

    public int MagicPoint { get; set; }

    public int GetCurrentMagicPoint()
    {
        return MagicPoint;
    }

    public void UseFireBall(int magicPointCost)
    {
        if (MagicPoint > magicPointCost)
        {
            MagicPoint -= magicPointCost;
        }
        else
        {
            throw new MagicPointNotEnoughException("Magic point not enough!");
        }
    }
}