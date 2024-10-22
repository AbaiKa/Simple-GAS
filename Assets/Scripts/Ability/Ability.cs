using UnityEngine;

public class Ability : MonoBehaviour
{
    public AbilityConfig Config {  get; private set; }
    public int CurrentCooldown { get; private set; }
    public Unit Owner { get; private set; }
    public Unit Target { get; private set; }

    public void Init(AbilityConfig config)
    {
        Config = config;
        CurrentCooldown = 0;

        ServicesAssistance.Main.Get<AdapterAssistance>().onBuildEffects.AddListener(Use);
    }
    public void DeInit()
    {
        ServicesAssistance.Main.Get<AdapterAssistance>().onBuildEffects.RemoveListener(Use);
        Owner.onTurn.RemoveListener(UpdateAbility);
        Destroy(gameObject);
    }
    public void SetOwner(Unit owner)
    {
        Owner = owner;
        Owner.onTurn.AddListener(UpdateAbility);
    }
    public void SetTarget(Unit target) 
    { 
        Target = target;
    }
    private void Use(BuildEffectData data)
    {
        if (data.OwnerId == Owner.Id && data.AbilityId == Config.Id)
        {
            CurrentCooldown = Config.Cooldown;

            BuildEffects(data.Properties);
        }
    }
    public bool IsAvailable()
    {
        if (Config.HasCooldown)
        {
            return CurrentCooldown == 0;
        }

        return true;
    }
    private void UpdateAbility()
    {
        if (Config.HasCooldown)
        {
            CurrentCooldown = Mathf.Max(0, CurrentCooldown - 1);
        }
    }

    private void BuildEffects(EffectProperties[] props)
    {
        for (int i = 0; i < props.Length; i++)
        {
            var effect = ServicesAssistance.Main.Get<EffectsBuilder>().Build(props[i].Type);
            var target = props[i].CastOnSelf ? Owner : Target;

            effect.Init(target, props[i]);
        }
    }
}
