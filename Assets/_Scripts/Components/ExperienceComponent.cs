using System;
using UnityEngine;
using UnityEngine.Serialization;

//GLOBAL EVENTS
public static class CombatEvents
{
    public static Action<int> OnEnemyKilled;
}

public class ExperienceComponent : MonoBehaviour
{
    #region Stats
    [SerializeField] private int ExperienceForLevelProgression; //TODO: How much to add to the "ExpToNextLvl"? Can this be collected in a file for level progression?
    [SerializeField, Min(0f)] private float expToNextLvl = 100f;
    public float ExpToNextLvl => expToNextLvl;
    public float CurrentExp { get; private set; }
    public float CurrentLvl { get; private set; }
    #endregion
    
    #region Events of the Experience Component
    private event Action MaxExpChanged; //in caso vogliamo modificare la salute massima in runtime
    private event Action ExpGained; //in caso vogliamo modificare la salute massima in runtime
    public event Action LevelUp; //Ho messo questo pubblico cos� posso gestire anche la cura tramite l'evento altrimenti non potevo usarlo
    #endregion
    
    private void Awake()
    {
        CurrentExp = 0;
        CurrentLvl = 1;
    }

    private void OnEnable()
    {
        CombatEvents.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        CombatEvents.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void Start()
    {
        Debug.Log($"ExperienceComponent initialized with ExpToNextLvl: {expToNextLvl} and CurrentExp: {CurrentExp}");
    }
    
    public void SetExpToNextLvl(float toAdd)
    {
        expToNextLvl += toAdd;
        MaxExpChanged?.Invoke();
        Debug.Log($"You are lvl: {CurrentLvl} - To next level: {ExpToNextLvl} XP");
    }
    
    public void GainExperience(float amount)
    {
        CurrentExp += amount;
        if (CurrentExp >= expToNextLvl)
            AddLevel();
        
        ExpGained?.Invoke();
        Debug.Log($"{gameObject.name} - Current experience: {CurrentExp}");
    }

    public void AddLevel()
    {
        CurrentExp = 0;
        SetExpToNextLvl(ExperienceForLevelProgression);
        CurrentLvl++;
        LevelUp?.Invoke();
    }
    
    private void HandleEnemyKilled(int expGained)
    {
        GainExperience(expGained);
    }
}
