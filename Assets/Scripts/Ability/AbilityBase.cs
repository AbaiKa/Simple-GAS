using UnityEngine;

public abstract class AbilityBase 
{
    #region Properties
    /// <summary>
    /// The main value of the ability
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("The main value of the ability")]
    public int Value { get; private set; }


    [SerializeField] private bool _hasDuration;
    /// <summary>
    /// Does the current ability have a duration
    /// </summary>
    public bool HasDuration => _hasDuration;

    /// <summary>
    /// Ability Duration
    /// </summary>
    [field: SerializeField, ShowIf("_hasDuration")]
    [field: Tooltip("Ability Duration")]
    public int Duration { get; private set; }


    [SerializeField] private bool _hasCooldown;
    /// <summary>
    /// Does the current ability have a cooldown
    /// </summary>
    public bool HasCooldown => _hasCooldown;

    /// <summary>
    /// Ability Cooldown
    /// </summary>
    [field: SerializeField, ShowIf("_hasCooldown")]
    [field: Tooltip("Ability Cooldown")]
    public int Cooldown { get; private set; }
    #endregion

    public Unit Owner { get; private set; }

    public void Init(Unit owner)
    {
        Owner = owner;
    }
    public void Use()
    {
        Logic();

        if (_hasDuration)
        {
            Duration = Mathf.Max(0, Duration - 1);
        }
    }

    protected bool IsComplete()
    {
        if (_hasDuration)
        {
            return Duration == 0;
        }

        return true;
    }

    protected abstract void Logic();
}