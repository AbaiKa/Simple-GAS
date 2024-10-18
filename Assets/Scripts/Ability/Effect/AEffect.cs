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
    public Sprite Icon { get; private set; }
    public int Value {  get; private set; }
    public int Duration { get; private set; }

    public UnityEvent<AEffect> onActivate;
    public UnityEvent<AEffect> onDeactivate;
    public UnityEvent<AEffect> onUse;

    protected Unit target;
    protected abstract void Logic();

    public void Activate(Unit target, Sprite icon, int value, int duration)
    {
        Icon = icon;
        Value = value;
        Duration = duration;
        this.target = target;
        this.target.onTurnStart.AddListener(Use);

        onActivate?.Invoke(this);
    }
    public void Cancel() 
    {
        onDeactivate?.Invoke(this); 
        target.onTurnStart.RemoveListener(Use);
        Destroy(gameObject); 
    }

    private void Use()
    {
        Logic();
        Duration = Mathf.Max(0, Duration - 1);
        
        if(Duration == 0)
        {
            Cancel();
        }

        onUse?.Invoke(this);
    }
}
