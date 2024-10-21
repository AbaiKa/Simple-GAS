using UnityEngine;

public class UnitsManager : MonoBehaviour, IService
{
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private SimpleEnemy enemyPrefab;
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;

    public Unit Player { get; private set; }
    public SimpleEnemy Enemy { get; private set; }

    public void Init()
    {
        Player = Instantiate(unitPrefab, spawnPointA.position, spawnPointA.rotation);
        Enemy = Instantiate(enemyPrefab, spawnPointB.position, spawnPointB.rotation);

        Player.Init();
        Enemy.Init();
    }

    public void DeInit()
    {
        Player.DeInit();
        Enemy.DeInit();
    }
}
