using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IService
{
    [SerializeField] private AbilityButton buttonPrefab;
    [SerializeField] private Transform abilitiesContainer;
    [SerializeField] private Button restartButton;

    private List<AbilityButton> abilityButtons = new List<AbilityButton>();

    public void Init(List<Ability> abilities)
    {
        SetActiveAbilitiesPanel(true);

        for (int i = 0; i < abilities.Count; i++)
        {
            var item = Instantiate(buttonPrefab, abilitiesContainer);
            item.Init(abilities[i]);
            abilityButtons.Add(item);
        }

        restartButton.onClick.AddListener(ServicesAssistance.Main.Get<GameProcessManager>().RestartGame);
    }

    public void DeInit()
    {
        for(int i = 0; i < abilityButtons.Count; i++)
        {
            abilityButtons[i].DeInit();
        }

        abilityButtons.Clear();

        restartButton.onClick.RemoveListener(ServicesAssistance.Main.Get<GameProcessManager>().RestartGame);
    }

    public void SetActiveAbilitiesPanel(bool value)
    {
        abilitiesContainer.gameObject.SetActive(value);
    }
}
