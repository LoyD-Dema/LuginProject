using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpCard : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image icon;

    public void SetupCard(PowerUpData data)
    {
        nameText.text = data.PowerUpName;
        icon.sprite = data.Icon;

        descriptionText.text = data.GetFormattedDescription(data.RollModifier(data.Modifier));
    }
}
