using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Button mButton;
    [SerializeField] private Image imageComponent;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private ProgressBarComponent barComponent;
    [SerializeField] private TextMeshProUGUI progressText;

    public UnityEvent onClick;
    private Ability ability;
    public void Init(Ability ability)
    {
        this.ability = ability;
        imageComponent.sprite = ability.Icon;
        titleText.text = ability.Title;

        if (ability.HasCooldown)
        {
            this.ability.Owner.onTurnEnd.AddListener(UpdateAbilityValues);
            UpdateAbilityValues();
        }
        else
        {
            progressText.text = "Доступно";
        }
        mButton.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        onClick?.Invoke();
    }
    private void UpdateAbilityValues()
    {
        barComponent.UpdateFill(ability.StartCooldown, ability.CurrentCooldown);

        if (ability.IsAvailable())
        {
            progressText.text = "Доступно";
        }
        else
        {
            progressText.text = $"Доступно через: {ability.CurrentCooldown} ходов";
        }
    }
}
