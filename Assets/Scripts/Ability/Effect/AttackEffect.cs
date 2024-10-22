public class AttackEffect : AEffect
{
    public override EffectType Type => EffectType.Attack;
    protected override void Logic()
    {
        ServicesAssistance.Main.Get<AdapterAssistance>().TakeDamage(target.Id, Value);
    }
}
