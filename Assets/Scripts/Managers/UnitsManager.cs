using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour, IService
{
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private SimpleEnemy enemyPrefab;
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;

    public Unit Player { get; private set; }
    public SimpleEnemy Enemy { get; private set; }
    public List<Unit> Units { get; private set; } = new List<Unit>();

    public void Init()
    {
        Player = Instantiate(unitPrefab, spawnPointA.position, spawnPointA.rotation);
        Enemy = Instantiate(enemyPrefab, spawnPointB.position, spawnPointB.rotation);

        Player.Init("Player_1", true);
        Enemy.Init("Player_2", false);

        Units.Add(Player);
        Units.Add(Enemy);
    }

    public void DeInit()
    {
        Player.DeInit();
        Enemy.DeInit();

        Units.Clear();
    }
}
