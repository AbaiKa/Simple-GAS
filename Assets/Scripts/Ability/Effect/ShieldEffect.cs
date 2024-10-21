using UnityEngine;

public class ShieldEffect : AEffect
{
    public override EffectType Type => EffectType.Shield;

    protected override void Logic()
    {
        Value = startValue;
    }
    public int GetBlockedDamage(int damage)
    {
        return Value - damage;
    }
}
