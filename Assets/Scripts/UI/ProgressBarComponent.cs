using UnityEngine;
using UnityEngine.UI;

public class ProgressBarComponent : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public void UpdateFill(float maxValue, float currentValue)
    {
        float onePercent = maxValue / 100;
        float result = currentValue / onePercent;
        float value = currentValue == 0 ? 0 : result / 100;

        fillImage.fillAmount = value;
    }
}
