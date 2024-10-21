using UnityEngine;

public class SimpleEnemy : Unit
{
    public override void TurnStart()
    {
        base.TurnStart();

        while (true)
        {
            int id = Random.Range(0, Abilities.Count);

            if (Abilities[id].IsAvailable())
            {
                var sendedData = new RequestData<EffectProperties[]>(Abilities[id].Config.Id, Abilities[id].Config.EffectProperties);
                ServicesAssistance.Main.Get<ServerAssistance>().SendAttackData(sendedData);
                TurnEnd();
                break;
            }
        }
    }
}
