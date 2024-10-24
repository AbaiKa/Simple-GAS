using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour, IService
{
    [SerializeField] private ServicesAssistance servicesAssistant;
    [SerializeField] private UnitsManager unitsAssistant;
    [SerializeField] private AbilitiesAssistance abilitiesAssistant;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameProcessManager gameProcessManager;
    [SerializeField] private EffectsNotificationBuilder effectsNotificationBuilder;

    private AbilitiesBuilder abilitiesBuilder;
    private EffectsBuilder effectsBuilder;
    private ServerAssistance serverAssistance;
    private AdapterAssistance adapter;
    private void Awake()
    {
        servicesAssistant.Init();
        
        abilitiesBuilder = new AbilitiesBuilder();
        effectsBuilder = new EffectsBuilder();
        serverAssistance = new ServerAssistance();
        adapter = new AdapterAssistance();

        servicesAssistant.Register(this);
        servicesAssistant.Register(unitsAssistant);
        servicesAssistant.Register(abilitiesBuilder);
        servicesAssistant.Register(effectsBuilder);
        servicesAssistant.Register(abilitiesAssistant);
        servicesAssistant.Register(turnManager);
        servicesAssistant.Register(uiManager);
        servicesAssistant.Register(gameProcessManager);
        servicesAssistant.Register(effectsNotificationBuilder);
        servicesAssistant.Register(serverAssistance);
        servicesAssistant.Register(adapter);

        StartCoroutine(InitRoutine());
    }

    public IEnumerator InitRoutine()
    {
        gameProcessManager.SetGameStatus(GameStatus.InProgress);

        unitsAssistant.Init();
        adapter.Init(serverAssistance);

        yield return new WaitForEndOfFrame();
        var playerAbilities = abilitiesAssistant.GetAbilities(unitsAssistant.Player, unitsAssistant.Enemy, false);
        var enemyAbilities = abilitiesAssistant.GetAbilities(unitsAssistant.Enemy, unitsAssistant.Player, true);

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < playerAbilities.Count; i++)
        {
            adapter.AddAbility(unitsAssistant.Player.Id, playerAbilities[i]);
        }
        for (int i = 0; i < enemyAbilities.Count; i++)
        {
            adapter.AddAbility(unitsAssistant.Enemy.Id, enemyAbilities[i]);
        }

        yield return new WaitForEndOfFrame();
        turnManager.Init(unitsAssistant.Units);

        yield return new WaitForEndOfFrame();
        uiManager.Init(playerAbilities);
    }

    public IEnumerator DeInitRoutine()
    {
        uiManager.DeInit();

        yield return new WaitForEndOfFrame();
        turnManager.DeInit();

        yield return new WaitForEndOfFrame();
        abilitiesAssistant.DeInit();

        yield return new WaitForEndOfFrame();
        adapter.DeInit();
        unitsAssistant.DeInit();
    }
}
