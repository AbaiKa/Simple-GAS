using UnityEngine;

public class UIManager : MonoBehaviour, IService
{
    [SerializeField] private AbilityButton buttonPrefab;
    [SerializeField] private Transform abilitiesContainer;

    public void Init(Ability[] abilities)
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            var item = Instantiate(buttonPrefab, abilitiesContainer);
            item.Init(abilities[i]);
        }
    }
}
