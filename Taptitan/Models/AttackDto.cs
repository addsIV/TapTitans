namespace Taptitan.Models;

public class AttackDto
{
    public int AttackPoint { get; set; }
    public int MagicPoint{ get; set; }
    
    public bool IsUseMagic{ get; set; }
    
    public Element Element{ get; set; }
    public bool IsFail { get; set; }
}