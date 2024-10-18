using UnityEngine;

public class ShieldEffect : AEffect
{
    public override EffectType Type => EffectType.Shield;

    protected override void Logic()
    {
        
    }
    public int GetBlockedDamage(int damage)
    {
        return Mathf.Max(0, damage - Value);
    }
}
