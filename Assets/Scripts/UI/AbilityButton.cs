using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Button mButton;
    [SerializeField] private Image imageComponent;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private ProgressBarComponent barComponent;
    [SerializeField] private TextMeshProUGUI progressText;

    private Ability ability;
    public void Init(Ability ability)
    {
        this.ability = ability;
        imageComponent.sprite = ability.Config.Icon;
        titleText.text = ability.Config.Title;

        if (ability.Config.HasCooldown)
        {
            this.ability.Owner.onTurnStart.AddListener(UpdateAbilityValues);
            UpdateAbilityValues();
        }
        else
        {
            progressText.text = "Доступно";
        }
        mButton.onClick.AddListener(OnClick);
    }
    public void DeInit()
    {
        if (ability.Config.HasCooldown)
        {
            ability.Owner.onTurnStart.RemoveListener(UpdateAbilityValues);
        }

        Destroy(gameObject);
    }
    private void OnClick()
    {
        if (ability.IsAvailable())
        {
            var sendedData = new RequestData<EffectProperties[]>(ability.Config.Id, ability.Config.EffectProperties);
            ServicesAssistance.Main.Get<ServerAssistance>().SendAttackData(sendedData);
        }
    }
    private void UpdateAbilityValues()
    {
        barComponent.UpdateFill(ability.Config.Cooldown, ability.CurrentCooldown);

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
