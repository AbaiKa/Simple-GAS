using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour, IService
{
    [Header("Properties")]
    [SerializeField] private bool isPlayerFirst;
    [Header("Events")]
    /// <summary>
    /// Called on every turn of the player and the enemy.
    /// </summary>
    public UnityEvent<Unit> onMove;


    private Unit player;
    private Unit enemy;

    private Unit currentUnit;
    private bool isPlayerTurn;
    public void StartGame(Unit player, Unit enemy)
    {
        this.player = player;
        this.enemy = enemy;

        isPlayerTurn = isPlayerFirst;
        currentUnit = isPlayerTurn ? this.player : this.enemy;

        TurnStart();
    }
    private void TurnStart()
    {
        currentUnit.TurnStart();

        onMove?.Invoke(currentUnit);
    }
    private void TurnEnd()
    {
        currentUnit.TurnEnd();
        
        isPlayerTurn = !isPlayerTurn;
        currentUnit = isPlayerTurn ? player : enemy;

        TurnStart();
    }

    private void TurnStop()
    {

    }
}
