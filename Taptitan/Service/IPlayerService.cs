namespace Taptitan.Service;

public interface IPlayerService
{
    int AttackPoint { get; set; }
    int MagicPoint { get; set; }
    int GetCurrentMagicPoint();
    void UseFireBall(int magicPointCost);
}