using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool HasEffect { get; private set; }
    public EffectProperties[] EffectProperties { get; private set; }
    public bool HasCooldown { get; private set; }
    public int StartCooldown { get; private set; }
    public int CurrentCooldown { get; private set; }
    public string Title { get; private set; }
    public Sprite Icon { get; private set; }
    public Unit Owner { get; private set; }
    public Unit Target { get; private set; }

    public void Init(AbilityConfig config)
    {
        HasEffect = config.HasEffect;
        EffectProperties = config.EffectProperties;
        HasCooldown = config.HasCooldown;
        StartCooldown = config.Cooldown;
        CurrentCooldown = 0;
        Title = config.Title;
        Icon = config.Icon;
    }
    public void DeInit()
    {
        Owner.onTurnEnd.RemoveListener(UpdateAbility);
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
        if (!IsAvailable())
        {
            Debug.LogWarning($"Ability not ready yet. Wait {CurrentCooldown} moves");
            return;
        }

        CurrentCooldown = StartCooldown;

        if (HasEffect)
        {
            for(int i = 0; i < EffectProperties.Length; i++)
            {
                var e = ServicesAssistance.Main.Get<EffectsBuilder>().Build(EffectProperties[i].Type);
                
                var target = EffectProperties[i].CastOnSelf ? Owner : Target;
                e.Activate(target, Icon, EffectProperties[i].Value, EffectProperties[i].Duration);
                target.AddEffect(e);
            }
        }
    }
    public bool IsAvailable()
    {
        if (HasCooldown)
        {
            return CurrentCooldown == 0;
        }

        return true;
    }
    private void UpdateAbility()
    {
        if (HasCooldown)
        {
            CurrentCooldown = Mathf.Max(0, CurrentCooldown - 1);
        }
    }
}
