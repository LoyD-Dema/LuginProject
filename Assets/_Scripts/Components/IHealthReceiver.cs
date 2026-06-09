using Utilities;

public enum HealthEffectType
{
    Damage,
    Heal
}

public struct HealthEffect
{
    public HealthEffectType Type;
    public float Amount;
}

public interface IHealthReceiver
{
    void ApplyEffect(HealthEffect effect);
}
