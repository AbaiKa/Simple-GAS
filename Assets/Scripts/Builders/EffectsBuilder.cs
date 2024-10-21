using System;
using UnityEngine;

public class EffectsBuilder : IService
{
    public AEffect Build(EffectType type)
    {
        GameObject item = new GameObject(type + "Effect");
        AEffect effect = (AEffect)item.AddComponent(GetEffectClass(type));
        return effect;
    }

    private Type GetEffectClass(EffectType type)
    {
        string className = type + "Effect";
        return Type.GetType(className);
    }
}

[Serializable]
public enum EffectType
{
    Attack,
    Shield,
    Healing,
    Burning,
    Cleaning,
}
