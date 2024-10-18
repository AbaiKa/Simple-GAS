using UnityEngine;
using UnityEngine.UI;

public class ProgressBarComponent : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public void UpdateFill(int maxValue, int currentValue)
    {
        float onePercent = maxValue / 100;
        float result = currentValue / onePercent;
        fillImage.fillAmount = result / 100;
    }
}
