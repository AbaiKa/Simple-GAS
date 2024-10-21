using UnityEngine;

public class AdapterAssistance : IService
{
    public string ToJson<T>(T data)
    {
        return JsonUtility.ToJson(data);
    }
    public T FromJson<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
    public T FromJsonOverwrite<T>(string json) where T : ScriptableObject
    {
        T t = ScriptableObject.CreateInstance<T>();
        JsonUtility.FromJsonOverwrite(json, t);
        return t;
    }
}
