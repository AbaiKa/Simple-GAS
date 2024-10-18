public class CleaningEffect : AEffect
{
    public override EffectType Type => EffectType.Cleaning;
    protected override void Logic()
    {
        target.RemoveEffect(EffectType.Burning);
    }
}
