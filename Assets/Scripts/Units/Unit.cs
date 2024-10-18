using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public bool IsAlive => health.Value > 0;

    [SerializeField] private ProgressBarComponent healthBar;
    [SerializeField] private TextMeshProUGUI healthTextComponent;
    [SerializeField] private EffectsNotificator effectsNotificatorPrefab;
    [SerializeField] private Transform effectsNotificationsContainer;
    private HealthComponent health;

    private List<AEffect> effects = new List<AEffect>();

    [Space]
    public UnityEvent onTurnStart;
    public UnityEvent onTurnEnd;

    #region Methods
    #region Public
    public void Init()
    {
        health = new HealthComponent(Random.Range(40, 50));
        health.onChanged.AddListener(UpdateHUD);
    }
    public virtual void TurnStart()
    {
        onTurnStart?.Invoke();
    }
    public virtual void TurnEnd()
    {
        onTurnEnd?.Invoke();
    }

    #region Health
    public void TakeDamage(int damage)
    {
        int currentDamage = 0;

        for (int i = 0; i < effects.Count; i++) 
        {
            if (effects[i].Type == EffectType.Shield)
            {
                currentDamage += (effects[i] as ShieldEffect).GetBlockedDamage(damage);
            }
        }

        health.Substract(currentDamage);
    }
    public void AddHealth(int value)
    {
        health.Add(value);
    }
    #endregion
    #region Ability & Effect
    public void AddEffect(AEffect effect)
    {
        effects.Add(effect);

        var e = Instantiate(effectsNotificatorPrefab, effectsNotificationsContainer);
        e.Init(effect.Icon, effect.Duration);

        effect.onUse.AddListener((c) => { e.SetInfo(c.Duration); });
        effect.onDeactivate.AddListener((c) => { e.DeInit(); });
    }
    public void RemoveEffect(EffectType type)
    {
        foreach (var e in effects)
        {
            if (e.Type == type)
            {
                effects.Remove(e);
                e.Cancel();
            }
        }
    }
    #endregion
    #endregion

    #region Private
    private void UpdateHUD(int max, int current)
    {
        healthBar.UpdateFill(max, current);
        healthTextComponent.text = current.ToString();
    }
    #endregion
    #endregion
}
