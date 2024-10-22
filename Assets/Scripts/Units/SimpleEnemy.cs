using System.Collections;
using UnityEngine;

public class SimpleEnemy : Unit
{
    public override IEnumerator Turn()
    {
        yield return base.Turn();

        while (true)
        {
            int id = Random.Range(0, Abilities.Count);

            if (Abilities[id].IsAvailable())
            {
                ServicesAssistance.Main.Get<TurnManager>().ChangeTurn(Id);
                ServicesAssistance.Main.Get<AdapterAssistance>().BuildEffect(Abilities[id].Owner.Id,
                    Abilities[id].Config.Id, Abilities[id].Config.EffectProperties);
                break;
            }

            yield return null;
        }
    }
}
