public class CleaningEffect : AEffect
{
    public override EffectType Type => EffectType.Cleaning;
    protected override void Logic()
    {
        ServicesAssistance.Main.Get<AdapterAssistance>().RemoveEffects(target.Id, EffectType.Burning);
    }
}
