using System.Collections;
using UnityEngine;

public class GameProcessManager : MonoBehaviour, IService
{
    public GameStatus Status { get; private set; }
    public void SetGameStatus(GameStatus status)
    {
        Status = status;

        if(status == GameStatus.Finish)
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        StartCoroutine(RestartGameRoutine());
    }
    private IEnumerator RestartGameRoutine()
    {
        yield return ServicesAssistance.Main.Get<GameManager>().DeInitRoutine();
        yield return new WaitForEndOfFrame();
        yield return ServicesAssistance.Main.Get<GameManager>().InitRoutine();
    }

}

public enum GameStatus
{
    InProgress,
    Finish
}
