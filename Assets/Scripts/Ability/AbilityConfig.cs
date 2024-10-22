using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ability", fileName = "Ability_")]
public class AbilityConfig : ScriptableObject
{
    [field: SerializeField] 
    public string Id { get; private set; }

    [SerializeField] private bool _hasEffect;
    /// <summary>
    /// Does the current ability have a duration
    /// </summary>
    public bool HasEffect => _hasEffect;

    [field: SerializeField, ShowIf("_hasEffect")]
    public EffectProperties[] EffectProperties { get; private set; }


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

    [field: Space()]
    [field: SerializeField]
    public string Title { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; private set; }
}

[Serializable]
public class EffectProperties
{
    /// <summary>
    /// Cast on self or enemy
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("Cast on self or enemy")]
    public bool CastOnSelf { get; private set; }
    /// <summary>
    /// Use in the next turn
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("Use in the next turn")]
    public bool UseInNextTurn { get; private set; }
    /// <summary>
    /// Effect type
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("Effect type")]
    public EffectType Type { get; private set; }
    /// <summary>
    /// Effect per duration value
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("Effect per move value")]
    public int Value { get; private set; }
    /// <summary>
    /// Effect duration
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("Effect duration")]
    public int Duration { get; private set; }
    /// <summary>
    /// Effect destroy time
    /// </summary>
    [field: SerializeField]
    [field: Tooltip("Effect destroy time")]
    public int DestroyTime { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; private set; }
}
