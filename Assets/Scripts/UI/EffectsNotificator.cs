using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectsNotificator : MonoBehaviour
{
    [SerializeField] private Image imageComponent;
    [SerializeField] private TextMeshProUGUI textComponent;

    public void Init(Sprite icon, int duration)
    {
        imageComponent.sprite = icon;
        SetInfo(duration);
    }
    public void SetInfo(int duration)
    {
        textComponent.text = duration.ToString();
    }
    public void DeInit()
    {
        Destroy(gameObject);
    }
}
