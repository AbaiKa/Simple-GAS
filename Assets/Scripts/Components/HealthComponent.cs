using UnityEngine;
using UnityEngine.Events;

public class HealthComponent
{
    /// <summary>
    /// Current health
    /// </summary>
    public int Value { get; private set; }
    /// <summary>
    /// Max health
    /// </summary>
    public int MaxValue { get; private set; }
    /// <summary>
    /// Calls when the health value changes
    /// <para> Argument 1: "start" Health</para>
    /// <para> Argument 2: "current" Health</para>
    /// </summary>
    public UnityEvent<int, int> onChanged;
    
    /// <summary>
    /// Set start health
    /// </summary>
    /// <param name="health"></param>
    public HealthComponent(int health)
    {
        Value = health;
        MaxValue = health;
    }

    /// <summary>
    /// Add health to this object 
    /// </summary>
    /// <param name="value"></param>
    public void Add(int value)
    {
        Value = Mathf.Min(MaxValue, Value + value);
        onChanged?.Invoke(MaxValue, Value);
    }
    /// <summary>
    /// Subtract health from this object
    /// </summary>
    /// <param name="value"></param>
    public void Substract(int value)
    {
        Value = Mathf.Max(0, Value - value);
        onChanged?.Invoke(MaxValue, Value);
    }
}