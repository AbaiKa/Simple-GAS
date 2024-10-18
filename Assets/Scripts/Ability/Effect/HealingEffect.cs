public class HealingEffect : AEffect
{
    public override EffectType Type => EffectType.Healing;
    protected override void Logic()
    {
        target.AddHealth(Value);
    }
}
