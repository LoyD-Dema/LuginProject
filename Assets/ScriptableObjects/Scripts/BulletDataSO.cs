using UnityEngine;

[CreateAssetMenu(fileName = "New BulletData", menuName = "ScriptableObject/BulletData", order = 0)]
public class BulletDataSO : ScriptableObject
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseDamage;
    [SerializeField] private int baseNumObjectToPirce;

    public float BaseSpeed => baseSpeed;
    public float BaseDamage => baseDamage;
    public int BaseNumObjectToPirce => baseNumObjectToPirce;
}
