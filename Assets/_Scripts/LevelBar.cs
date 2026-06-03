using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;

    [Header("Parameters")]
    [SerializeField] float experienceRequiredMultiplayer;
    [SerializeField] float[] experienceRequiredToLevelUp;

    private int level;
    private float experienceLeftOver;
    private float currentExperience;
    private float currentExperienceRequiredToLevelUp;

    public event Action OnLevelUp;

    private void Start()
    {
        if (experienceRequiredToLevelUp.Length <= 0)
        {
            Debug.LogError("Add at least one element in the array to have the first target to level up", this);
            return;
        }

        currentExperienceRequiredToLevelUp = experienceRequiredToLevelUp[0];
    }

    public void AddExp(float amount)
    {
        currentExperience += amount;

        // Update UI
        UpdateSlider();

        // For Debug
        Debug.Log(currentExperience + " / " + currentExperienceRequiredToLevelUp);

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
            text.text = level.ToString();
            UpdateSlider();

            OnLevelUp?.Invoke();
        }
    }

    private void UpdateSlider()
    {
        slider.value = Mathf.Min(currentExperience / currentExperienceRequiredToLevelUp, currentExperienceRequiredToLevelUp);
    }
}
