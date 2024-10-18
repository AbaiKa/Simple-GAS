using System;
using System.Collections.Generic;
using UnityEngine;

public class ServicesAssistance : MonoBehaviour 
{
    public static ServicesAssistance Main {  get; private set; }

    private readonly Dictionary<string, IService> services = new Dictionary<string, IService>();

    public void Init()
    {
        Main = this;
    }
    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (services.ContainsKey(key)) 
        {
            Debug.LogError($"Service '{key}' is already registered");
            return;
        }

        services.Add(key, service);
    }
    public void Unregister<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (!services.ContainsKey(key))
        {
            Debug.Log($"Service '{key}' not yet registered");
            return;
        }

        services.Remove(key);
    }
    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;

        if (!services.ContainsKey(key)) 
        {
            string msg = $"There is no '{key}' service";
            Debug.LogError(msg);
            throw new InvalidOperationException(msg);
        }

        return (T)services[key];
    }
}
