using UnityEngine;

public class AbilitiesBuilder : IService
{
    public Ability Build(AbilityConfig config)
    {
        GameObject item = new GameObject(config.name + "Ability");
        Ability ability = item.AddComponent<Ability>();
        
        return ability;
    }
}
