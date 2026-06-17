using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Damage,
    MoveSpeed,
    Bullet
}
[CreateAssetMenu(fileName = "New Power Up", menuName = "Power Up")]
public class PowerUpData : ScriptableObject
{
    [SerializeField] private string powerUpName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    [SerializeField] private StatModifier modifier;

    public string PowerUpName => powerUpName;
    public string Description => description;
    public Sprite Icon => icon;
    public StatModifier Modifier => modifier;


    public float RollModifier(StatModifier modifier)
    {
        return Random.Range(modifier.minIncrease, modifier.maxIncrease);
    }

    public string GetFormattedDescription(float modifierValue)
    {
        int intValue = Mathf.RoundToInt(modifierValue * 100);

        return string.Format(description, intValue);
    }
}

[System.Serializable]
public class StatModifier
{
    public StatType statType;

    public float minIncrease = 0.02f;

    public float maxIncrease = 0.10f;

    public float CapPercentage = 0.30f;

    public BulletType bulletType = BulletType.Normal;
}
