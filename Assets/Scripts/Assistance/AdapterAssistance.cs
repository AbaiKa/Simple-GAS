using System;
using UnityEngine;
using UnityEngine.Events;

public class AdapterAssistance : IService
{
    public UnityEvent<AddAbilityData> onAddAbility = new UnityEvent<AddAbilityData>();
    /// <summary>
    /// <para>Param 1 - target id</para>
    /// <para>Param 2 - effect data</para>
    /// </summary>
    public UnityEvent<BuildEffectData> onBuildEffects = new UnityEvent<BuildEffectData>();
    /// <summary>
    /// <para>Param 1 - target id</para>
    /// <para>Param 2 - effect data</para>
    /// </summary>
    public UnityEvent<AddEffectData> onAddEffects = new UnityEvent<AddEffectData>();
    /// <summary>
    /// <para>Param 1 - target id</para>
    /// <para>Param 2 - effect data</para>
    /// </summary>
    public UnityEvent<RemoveEffectData> onRemoveEffects = new UnityEvent<RemoveEffectData>();
    /// <summary>
    /// Param - turn owner id
    /// </summary>
    public UnityEvent<string> onUpdateTurn = new UnityEvent<string>();
    public UnityEvent<HealthData> onTakeDamage = new UnityEvent<HealthData>();
    public UnityEvent<HealthData> onAddHealth = new UnityEvent<HealthData>();


    private ServerAssistance server;

    private readonly string addAbilityKey = "AddAbilityKey";
    private readonly string buildEffectKey = "BuildEffectKey";
    private readonly string addEffectKey = "AddEffectKey";
    private readonly string removeEffectsKey = "RemoveEffectsKey";
    private readonly string updateTurnKey = "UpdateTurnKey";
    private readonly string takeDamageKey = "TakeDamageKey";
    private readonly string addHealthKey = "addHealthKey";
    public void Init(ServerAssistance server)
    {
        this.server = server;
        this.server.onServerChange.AddListener(OnServerChanged);
    }
    public void DeInit()
    {
        server.onServerChange.RemoveListener(OnServerChanged);
    }
    private void OnServerChanged(string json)
    {
        if (json.Contains(addAbilityKey))
        {
            ServerData<AddAbilityData> responce = FromJson<ServerData<AddAbilityData>>(json);
            onAddAbility?.Invoke(responce.Data);
        }
        else if (json.Contains(buildEffectKey))
        {
            ServerData<BuildEffectData> responce = FromJson<ServerData<BuildEffectData>>(json);
            onBuildEffects?.Invoke(responce.Data);
        }
        else if (json.Contains(addEffectKey))
        {
            ServerData<AddEffectData> responce = FromJson<ServerData<AddEffectData>>(json);
            onAddEffects?.Invoke(responce.Data);
        }
        else if (json.Contains(removeEffectsKey))
        {
            ServerData<RemoveEffectData> responce = FromJson<ServerData<RemoveEffectData>>(json);
            onRemoveEffects?.Invoke(responce.Data);
        }
        else if (json.Contains(updateTurnKey))
        {
            ServerData<string> responce = FromJson<ServerData<string>>(json);
            onUpdateTurn?.Invoke(responce.Data);
        }
        else if (json.Contains(takeDamageKey))
        {
            ServerData<HealthData> responce = FromJson<ServerData<HealthData>>(json);
            onTakeDamage?.Invoke(responce.Data);
        }
        else if (json.Contains(addHealthKey))
        {
            ServerData<HealthData> responce = FromJson<ServerData<HealthData>>(json);
            onAddHealth?.Invoke(responce.Data);
        }
    }

    public void AddAbility(string unitId, Ability ability)
    {
        ServerData<AddAbilityData> data = new ServerData<AddAbilityData>(addAbilityKey, new AddAbilityData(unitId, ability));
        string json = ToJson(data);

        server.Send(json);
    }
    public void BuildEffect(string unitId, string abilityId, EffectProperties[] effects)
    {
        ServerData<BuildEffectData> data = new ServerData<BuildEffectData>(buildEffectKey, new BuildEffectData(unitId, abilityId, effects));
        string json = ToJson(data);

        server.Send(json);
    }
    public void AddEffect(string unitId, AEffect effect)
    {
        ServerData<AddEffectData> data = new ServerData<AddEffectData>(addEffectKey, new AddEffectData(unitId, effect));
        string json = ToJson(data);

        server.Send(json);
    }
    public void RemoveEffects(string unitId, EffectType type)
    {
        ServerData<RemoveEffectData> data = new ServerData<RemoveEffectData>(removeEffectsKey, new RemoveEffectData(unitId, type));
        string json = ToJson(data);

        server.Send(json);
    }
    public void RemoveEffects(string unitId, AEffect effect)
    {
        ServerData<RemoveEffectData> data = new ServerData<RemoveEffectData>(removeEffectsKey, new RemoveEffectData(unitId, effect));
        string json = ToJson(data);

        server.Send(json);
    }
    public void UpdateTurn(string unitId)
    {
        ServerData<string> data = new ServerData<string>(updateTurnKey, unitId);
        string json = ToJson(data);
        server.Send(json);
    }

    public void TakeDamage(string unitId, int value)
    {
        ServerData<HealthData> data = new ServerData<HealthData>(takeDamageKey, new HealthData(unitId, value));
        string json = ToJson(data);
        server.Send(json);
    }
    public void AddHealth(string unitId, int value)
    {
        ServerData<HealthData> data = new ServerData<HealthData>(addHealthKey, new HealthData(unitId, value));
        string json = ToJson(data);
        server.Send(json);
    }

    private string ToJson<T>(T data)
    {
        return JsonUtility.ToJson(data);
    }
    private T FromJson<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
    private T FromJsonOverwrite<T>(string json) where T : ScriptableObject
    {
        T t = ScriptableObject.CreateInstance<T>();
        JsonUtility.FromJsonOverwrite(json, t);
        return t;
    }
}

[Serializable]
public class ServerData<T>
{
    public string Id;
    public T Data;

    public ServerData(string id, T data)
    {
        Id = id;
        Data = data;
    }
}

[Serializable]
public class AddAbilityData
{
    public string OwnerId;
    public Ability Ability;

    public AddAbilityData(string ownerId, Ability ability)
    {
        OwnerId = ownerId;
        Ability = ability;
    }
}
[Serializable]
public class BuildEffectData
{
    public string OwnerId;
    public string AbilityId;
    public EffectProperties[] Properties;

    public BuildEffectData(string ownerId, string abilityId, EffectProperties[] properties)
    {
        OwnerId = ownerId;
        AbilityId = abilityId;
        Properties = properties;
    }
}
[Serializable]
public class AddEffectData
{
    public string OwnerId;
    public AEffect Effect;

    public AddEffectData(string ownerId, AEffect effect)
    {
        OwnerId = ownerId;
        Effect = effect;
    }
}
[Serializable]
public class RemoveEffectData
{
    public string Id;
    public EffectType Type;
    public AEffect Effect;

    public RemoveEffectData(string id, AEffect effect)
    {
        Id = id;
        Effect = effect;
        Type = EffectType.None;
    }
    public RemoveEffectData(string id, EffectType type)
    {
        Id = id;
        Type = type;
        Effect = null;
    }
}

[Serializable]
public class HealthData
{
    public string OwnerId;
    public int Value;

    public HealthData(string ownerId, int value)
    {
        OwnerId = ownerId;
        Value = value;
    }
}