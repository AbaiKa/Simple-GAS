using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for effects. 
/// Each effect class name must end with the 'Effect' prefix. 
/// Each new effect type must be added to the Enum 'EffectType' list.
/// </summary>
public abstract class AEffect : MonoBehaviour
{
    public abstract EffectType Type { get; }
    public Sprite Icon { get; protected set; }
    public int Value { get; protected set; }
    public int Duration { get; protected set; }

    public UnityEvent<AEffect> onActivate = new UnityEvent<AEffect>();
    public UnityEvent<AEffect> onDeactivate = new UnityEvent<AEffect>();
    public UnityEvent<AEffect> onUse = new UnityEvent<AEffect>();

    protected Unit target;
    protected int startValue { get; private set; }
    protected EffectProperties properties { get; private set; }

    protected int turnsToStart;

    protected abstract void Logic();

    public void Init(Unit target, Sprite icon, EffectProperties properties)
    {
        Icon = icon;
        Value = properties.Value;
        Duration = properties.Duration;

        startValue = properties.Value;
        this.properties = properties;

        this.target = target;
        this.target.AddEffect(this);
        this.target.HUD.AddEffectNotificator(this);
        this.target.onTurnStart.AddListener(Use);

        turnsToStart = properties.UseInNextTurn ? 1 : 0;
    }
    public void Cancel()
    {
        StartCoroutine(CancelRoutine());
    }
    private IEnumerator CancelRoutine()
    {
        onDeactivate?.Invoke(this);
        target.onTurnStart.RemoveListener(Use);
        yield return new WaitForSeconds(properties.DestroyTime);
        target.RemoveEffectFromList(this);
        Destroy(gameObject);
    }
    private void Use()
    {
        if (turnsToStart == 0)
        {
            Logic();
            Duration = Mathf.Max(0, Duration - 1);

            onUse?.Invoke(this);

            if (Duration == 0)
            {
                Cancel();
            }
        }
        else
        {
            turnsToStart = Mathf.Max(0, turnsToStart - 1);
        }
    }
}
