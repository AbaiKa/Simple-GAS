using UnityEngine;

public class UnitsManager : MonoBehaviour, IService
{
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;

    public Unit Player { get; private set; }
    public Unit Enemy { get; private set; }

    public void Init()
    {
        Player = Instantiate(unitPrefab, spawnPointA.position, spawnPointA.rotation);
        Enemy = Instantiate(unitPrefab, spawnPointB.position, spawnPointB.rotation);
    }
}
