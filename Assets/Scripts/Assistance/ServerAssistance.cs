using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ServerAssistance : IService
{
    public UnityEvent<string> onServerChange;
    private AdapterAssistance adapter;

    public void Init(AdapterAssistance adapter)
    {
        this.adapter = adapter;
    }

    public IEnumerator SendAttackData(RequestData<EffectProperties[]> data)
    {
        string json = adapter.ToJson(data);
        yield return new WaitForEndOfFrame(); 

        onServerChange?.Invoke(json);
    }
}

public class RequestData<T>
{
    public string Id { get; private set; }
    public T Data { get; private set; }

    public RequestData(string id, T data)
    {
        Id = id;
        Data = data;
    }
}
