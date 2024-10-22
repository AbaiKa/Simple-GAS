public class BurningEffect : AEffect
{
    public override EffectType Type => EffectType.Burning;
    protected override void Logic()
    {
        ServicesAssistance.Main.Get<AdapterAssistance>().TakeDamage(target.Id, Value);
    }
}
