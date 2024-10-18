public class BurningEffect : AEffect
{
    public override EffectType Type => EffectType.Burning;
    protected override void Logic()
    {
        target.TakeDamage(Value);
    }
}
