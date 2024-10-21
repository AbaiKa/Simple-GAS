using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour, IService
{
    private Unit player;
    private Unit enemy;

    private Unit currentUnit;
    private bool isPlayerTurn;
    private bool inProgress;
    public void Init(Unit player, Unit enemy)
    {
        inProgress = false;
        isPlayerTurn = false;

        this.player = player;
        this.enemy = enemy;

        this.enemy.onTurnEnd.AddListener(StartPlayerTurn);

        StartPlayerTurn();
    }
    public void DeInit()
    {
        StopAllCoroutines();
        enemy.onTurnEnd.RemoveListener(StartPlayerTurn);
    }
    public void StartPlayerTurn()
    {
        if (ServicesAssistance.Main.Get<GameProcessManager>().Status == GameStatus.Finish)
        {
            return;
        }

        ChangeTurn();

        Turn();
    }
    public void StartEnemyTurn()
    {
        if (ServicesAssistance.Main.Get<GameProcessManager>().Status == GameStatus.Finish)
        {
            return;
        }

        if (!inProgress)
        {
            inProgress = true;
            StartCoroutine(EnemyTurnRoutine());
        }
    }
    private IEnumerator EnemyTurnRoutine()
    {
        ChangeTurn();

        yield return new WaitForSeconds(Random.Range(1, 3));

        Turn();

        inProgress = false;
    }
    private void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        currentUnit = isPlayerTurn ? player : enemy;

        ServicesAssistance.Main.Get<UIManager>().SetActiveAbilitiesPanel(isPlayerTurn);
    }
    private void Turn()
    {
        currentUnit.TurnStart();
    }
}
