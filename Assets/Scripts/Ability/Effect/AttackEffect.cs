public class AttackEffect : AEffect
{
    public override EffectType Type => EffectType.Attack;
    protected override void Logic()
    {
        target.TakeDamage(Value);
    }
}
