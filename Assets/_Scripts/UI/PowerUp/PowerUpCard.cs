using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpCard : MonoBehaviour
{
    [SerializeField] private PowerUpManager powerUpManager;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image icon;

    private PowerUpData data;
    private float value;

    public void SetupCard(PowerUpData data)
    {
        this.data = data;
        value = data.RollModifier(data.Modifier);

        nameText.text = data.PowerUpName;
        icon.sprite = data.Icon;

        descriptionText.text = data.GetFormattedDescription(value);
    }

    public void OnCardSelected()
    {
        powerUpManager.ApplyPowerUp(data, value);
    }
}
