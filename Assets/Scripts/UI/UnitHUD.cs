using TMPro;
using UnityEngine;

public class UnitHUD : MonoBehaviour
{
    [SerializeField] private ProgressBarComponent healthBar;
    [SerializeField] private TextMeshProUGUI healthTextComponent;
    [SerializeField] private Transform effectsNotificationContainer;
    public void UpdateHealthBar(int max, int current)
    {
        healthBar.UpdateFill(max, current);
        healthTextComponent.text = current.ToString();
    }

    public EffectsNotificator AddEffectNotificator(AEffect effect)
    {
        return ServicesAssistance.Main.Get<EffectsNotificationBuilder>().Build(effect, effectsNotificationContainer);
    }
}
