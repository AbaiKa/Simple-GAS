using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ServicesAssistance servicesAssistant;
    [SerializeField] private UnitsManager unitsAssistant;
    [SerializeField] private AbilitiesAssistance abilitiesAssistant;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private UIManager uiManager;

    private AbilitiesBuilder abilitiesBuilder;
    private EffectsBuilder effectsBuilder;
    private void Awake()
    {
        servicesAssistant.Init();
        
        abilitiesBuilder = new AbilitiesBuilder();
        effectsBuilder = new EffectsBuilder();

        servicesAssistant.Register(unitsAssistant);
        servicesAssistant.Register(abilitiesBuilder);
        servicesAssistant.Register(effectsBuilder);
        servicesAssistant.Register(abilitiesAssistant);
        servicesAssistant.Register(turnManager);
        servicesAssistant.Register(uiManager);

        unitsAssistant.Init();

        var abilities = abilitiesAssistant.GetAbilities(unitsAssistant.Player, unitsAssistant.Enemy);

        uiManager.Init(abilities);
    }
}
