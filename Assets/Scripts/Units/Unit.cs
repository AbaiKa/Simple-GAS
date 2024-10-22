using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public string Id {  get; private set; }
    public bool IsPlayer { get; private set; }
    public bool IsAlive => health.Value > 0;

    [field: SerializeField] public UnitHUD HUD {  get; private set; }
    [Space]
    public UnityEvent onTurn = new UnityEvent();

    private HealthComponent health;

    [field: SerializeField] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    [field: SerializeField] public List<AEffect> Effects { get; private set; } = new List<AEffect>();

    #region Methods
    #region Public
    public void Init(string id, bool isPlayer)
    {
        Id = id;
        IsPlayer = isPlayer;
        health = new HealthComponent(50);
        health.onChanged.AddListener(HUD.UpdateHealthBar);
        health.onDead.AddListener(OnDead);

        ServicesAssistance.Main.Get<AdapterAssistance>().onAddAbility.AddListener(AddAbility);
        ServicesAssistance.Main.Get<AdapterAssistance>().onAddEffects.AddListener(AddEffect);
        ServicesAssistance.Main.Get<AdapterAssistance>().onRemoveEffects.AddListener(RemoveEffects);

        ServicesAssistance.Main.Get<AdapterAssistance>().onTakeDamage.AddListener(TakeDamage);
        ServicesAssistance.Main.Get<AdapterAssistance>().onAddHealth.AddListener(AddHealth);

        HUD.UpdateHealthBar(health.MaxValue, health.Value);
    }
    public void DeInit()
    {
        health.onChanged.RemoveListener(HUD.UpdateHealthBar);
        health.onDead.RemoveListener(OnDead);

        ServicesAssistance.Main.Get<AdapterAssistance>().onAddAbility.RemoveListener(AddAbility);
        ServicesAssistance.Main.Get<AdapterAssistance>().onAddEffects.RemoveListener(AddEffect);
        ServicesAssistance.Main.Get<AdapterAssistance>().onRemoveEffects.RemoveListener(RemoveEffects);

        ServicesAssistance.Main.Get<AdapterAssistance>().onTakeDamage.RemoveListener(TakeDamage);
        ServicesAssistance.Main.Get<AdapterAssistance>().onAddHealth.RemoveListener(AddHealth);

        Destroy(gameObject);
    }
    public virtual IEnumerator Turn()
    {
        onTurn?.Invoke();
        yield return null;
    }

    #region Health
    private void TakeDamage(HealthData data)
    {
        if (data.OwnerId == Id)
        {
            int currentDamage = data.Value;

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
    }
    private void AddHealth(HealthData data)
    {
        if (data.OwnerId == Id)
        {
            health.Add(data.Value);
        }
    }
    #endregion
    #region Ability & Effect
    private void AddAbility(AddAbilityData data)
    {
        if (data.OwnerId == Id)
        {
            Abilities.Add(data.Ability);
        }
    }
    private void AddEffect(AddEffectData data)
    {
        if (Id == data.OwnerId)
        {
            Effects.Add(data.Effect);
        }
    }
    private void RemoveEffects(RemoveEffectData data)
    {
        if (Id == data.Id)
        {
            if (Effects.Count > 0)
            {
                if (data.Type != EffectType.None)
                {
                    var removeEffects = Effects.Where(e => e.Type == data.Type).ToArray();

                    for (int i = 0; i < removeEffects.Length; i++)
                    {
                        removeEffects[i].Cancel();
                    }
                }
                else
                {
                    if (Effects.Contains(data.Effect))
                    {
                        Effects.Remove(data.Effect);
                    }
                }
            }
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
