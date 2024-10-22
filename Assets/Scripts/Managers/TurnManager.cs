using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, IService
{
    private List<Unit> units = new List<Unit>();

    private bool inProgress;
    public void Init(List<Unit> units)
    {
        inProgress = false;

        this.units = units;

        ServicesAssistance.Main.Get<AdapterAssistance>().onUpdateTurn.AddListener(Turn);
    }
    public void DeInit()
    {
        StopAllCoroutines();
        ServicesAssistance.Main.Get<AdapterAssistance>().onUpdateTurn.RemoveListener(Turn);
    }
    public void ChangeTurn(string id)
    {
        if (ServicesAssistance.Main.Get<GameProcessManager>().Status == GameStatus.Finish)
        {
            return;
        }

        Unit currentUnit = null;
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].Id != id)
            {
                currentUnit = units[i];
                break;
            }
        }

        if (currentUnit != null)
        {
            ServicesAssistance.Main.Get<AdapterAssistance>().UpdateTurn(currentUnit.Id);
        }
    }
    private void Turn(string id)
    {
        if (!inProgress)
        {
            inProgress = true;
            StartCoroutine(TurnRoutine(id));
        }
    }
    private IEnumerator TurnRoutine(string id)
    {
        var currentUnit = GetCurrentUnit(id);

        if (currentUnit != null)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));

            inProgress = false;

            ServicesAssistance.Main.Get<UIManager>().SetActiveAbilitiesPanel(currentUnit.IsPlayer);
            yield return currentUnit.Turn();
        }
        inProgress = false;
    }

    private Unit GetCurrentUnit(string id)
    {
        Unit currentUnit = null;

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].Id == id)
            {
                currentUnit = units[i];
                break;
            }
        }

        return currentUnit;
    }
}
