using UnityEngine;

public class EffectsNotificationBuilder : MonoBehaviour, IService
{
    [SerializeField] private EffectsNotificator notificatorPrefab;

    public EffectsNotificator Build(AEffect effect, Transform container)
    {
        var e = Instantiate(notificatorPrefab, container);
        e.Init(effect.Icon, effect.Duration);

        effect.onUse.AddListener((c) => { e.SetInfo(c.Duration); });
        effect.onDeactivate.AddListener((c) => { e.DeInit(); });

        return e;
    }
}
