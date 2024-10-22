public class HealingEffect : AEffect
{
    public override EffectType Type => EffectType.Healing;
    protected override void Logic()
    {
        ServicesAssistance.Main.Get<AdapterAssistance>().AddHealth(target.Id, Value);
    }
}
