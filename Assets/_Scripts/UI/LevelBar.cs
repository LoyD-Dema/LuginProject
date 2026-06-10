
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text levelText;

    [Header("Parameters")]
    [SerializeField] float experienceRequiredMultiplayer;
    [SerializeField] float[] experienceRequiredToLevelUp;

    private int level;
    public int Level => level;

    private float experienceLeftOver;
    private float currentExperience;
    private float currentExperienceRequiredToLevelUp;

    public event Action OnLevelUp;

    


#if UNITY_EDITOR
    // Da vedere
    [Header("Debug")]
    [SerializeField] bool enableDebugMessages;
#endif

    private void OnEnable()
    {
        CombatEvents.OnEnemyKilled += CombatEvent_OnEnemyKilled;
    }

    private void OnDisable()
    {
        CombatEvents.OnEnemyKilled -= CombatEvent_OnEnemyKilled;
    }

    private void CombatEvent_OnEnemyKilled(int exp)
    {
        AddExp(exp);
    }

    private void Start()
    {
        if (experienceRequiredToLevelUp.Length <= 0)
        {
            Debug.LogError("Add at least one element in the array to have the first target to level up", this);
            return;
        }

        currentExperienceRequiredToLevelUp = experienceRequiredToLevelUp[0];
        level = 0;
        currentExperience = 0;
        levelText.text = level.ToString();
        UpdateSlider();
    }


    public void AddExp(float amount)
    {
        currentExperience += amount;

        // Update UI
        UpdateSlider();


#if UNITY_EDITOR
        // For Debug
        if (enableDebugMessages)
        {
            Debug.Log(currentExperience + " / " + currentExperienceRequiredToLevelUp);
        }
#endif


        if (currentExperience >= currentExperienceRequiredToLevelUp)
        {
            experienceLeftOver = currentExperience - currentExperienceRequiredToLevelUp;
            currentExperience = experienceLeftOver;
            level++;

            if (level < experienceRequiredToLevelUp.Length)
            {
                currentExperienceRequiredToLevelUp = experienceRequiredToLevelUp[level];
            }
            else
            {
                currentExperienceRequiredToLevelUp *= experienceRequiredMultiplayer;
            }

            // Update UI
            levelText.text = level.ToString();
            UpdateSlider();

            OnLevelUp?.Invoke();
        }
    }

    private void UpdateSlider()
    {
        slider.value = Mathf.Min(currentExperience / currentExperienceRequiredToLevelUp, currentExperienceRequiredToLevelUp);
    }
}
