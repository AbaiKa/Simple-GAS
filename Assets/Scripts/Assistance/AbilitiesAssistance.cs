using UnityEngine;

public class AbilitiesAssistance : MonoBehaviour, IService
{
    [SerializeField] private AbilityConfig[] configs;

    public Ability[] GetAbilities(Unit owner, Unit target)
    {
        Ability[] abilities = new Ability[configs.Length];

        for (int i = 0; i < configs.Length; i++)
        {
            var ability = ServicesAssistance.Main.Get<AbilitiesBuilder>().Build(configs[i]);
            ability.Init(configs[i]);
            ability.SetOwner(owner);
            ability.SetTarget(target);

            abilities[i] = ability;
        }

        return abilities;
    }
}
