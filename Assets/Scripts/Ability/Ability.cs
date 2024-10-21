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
    }
    public void DeInit()
    {
        Owner.onTurnEnd.RemoveListener(UpdateAbility);
        Destroy(gameObject);
    }
    public void SetOwner(Unit owner)
    {
        Owner = owner;
        Owner.onTurnStart.AddListener(UpdateAbility);
    }
    public void SetTarget(Unit target) 
    { 
        Target = target;
    }
    public void Use()
    {
        CurrentCooldown = Config.Cooldown;

        BuildEffects();
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

    private void BuildEffects()
    {
        if (Config.HasCooldown)
        {
            var props = Config.EffectProperties;
            for (int i = 0; i < props.Length; i++)
            {
                var effect = ServicesAssistance.Main.Get<EffectsBuilder>().Build(props[i].Type);
                var target = props[i].CastOnSelf ? Owner : Target;

                effect.Init(target, Config.Icon, props[i]);
            }
        }
    }
}
