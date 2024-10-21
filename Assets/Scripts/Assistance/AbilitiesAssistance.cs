using System.Collections.Generic;
using UnityEngine;

public class AbilitiesAssistance : MonoBehaviour, IService
{
    [SerializeField] private AbilityConfig[] playerConfigs;
    [SerializeField] private AbilityConfig[] enemyConfigs;

    private List<Ability> abilities = new List<Ability>();

    public List<Ability> GetAbilities(Unit owner, Unit target, bool isEnemy)
    {
        var a = GetAbilities(owner, target, isEnemy ? enemyConfigs : playerConfigs);
        abilities.AddRange(a);

        return a;
    }
    private List<Ability> GetAbilities(Unit owner, Unit target, AbilityConfig[] configs)
    {
        List<Ability> abilities = new List<Ability>();

        for (int i = 0; i < configs.Length; i++)
        {
            var ability = ServicesAssistance.Main.Get<AbilitiesBuilder>().Build(configs[i]);
            ability.Init(configs[i]);
            ability.SetOwner(owner);
            ability.SetTarget(target);

            abilities.Add(ability);
        }

        return abilities;
    }

    public void DeInit()
    {
        for(int i =  0; i < abilities.Count; i++)
        {
            abilities[i].DeInit();
        }

        abilities.Clear();
    }
}
