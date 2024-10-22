using UnityEngine.Events;

public class ServerAssistance : IService
{
    public UnityEvent<string> onServerChange = new UnityEvent<string>();
    public void Send(string send)
    {
        onServerChange?.Invoke(send);
    }
}
