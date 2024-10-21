using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public bool IsAlive => health.Value > 0;

    [field: SerializeField] public UnitHUD HUD {  get; private set; }
    [Space]
    public UnityEvent onTurnStart = new UnityEvent();
    public UnityEvent onTurnEnd = new UnityEvent();

    private HealthComponent health;

    [field: SerializeField] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    [field: SerializeField] public List<AEffect> Effects { get; private set; } = new List<AEffect>();

    #region Methods
    #region Public
    public void Init()
    {
        health = new HealthComponent(Random.Range(40, 50));
        health.onChanged.AddListener(HUD.UpdateHealthBar);
        health.onDead.AddListener(OnDead);

        HUD.UpdateHealthBar(health.MaxValue, health.Value);
    }
    public void DeInit()
    {
        health.onChanged.RemoveListener(HUD.UpdateHealthBar);
        health.onDead.RemoveListener(OnDead);
        Destroy(gameObject);
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
        int currentDamage = damage;

        if (Effects.Count > 0)
        {
            var shieldsList = Effects.OfType<ShieldEffect>().ToArray();

            for (int i = 0; i < shieldsList.Length; i++)
            {
                int block = shieldsList[i].GetBlockedDamage(currentDamage);
                currentDamage -= (currentDamage + block);
            }
        }

        currentDamage = currentDamage > 0 ? currentDamage : 0;
        health.Substract(currentDamage);
    }
    public void AddHealth(int value)
    {
        health.Add(value);
    }
    #endregion
    #region Ability & Effect
    public void SetAbilities(List<Ability> abilities)
    {
        Abilities = abilities;
    }
    public void AddEffect(AEffect aEffect)
    {
        Effects.Add(aEffect);
    }
    public void RemoveEffects(EffectType type)
    {
        if (Effects.Count > 0)
        {
            var removeEffects = Effects.Where(e => e.Type == type).ToArray();

            for(int i = 0; i < removeEffects.Length; i++)
            {
                removeEffects[i].Cancel();
            }
        }
    }
    public void RemoveEffectFromList(AEffect effect)
    {
        if (Effects.Contains(effect))
        {
            Effects.Remove(effect);
        }
    }
    #endregion
    #endregion

    #region Protected
    protected virtual void OnDead()
    {
        ServicesAssistance.Main.Get<GameProcessManager>().SetGameStatus(GameStatus.Finish);
    }
    #endregion

    #endregion
}
