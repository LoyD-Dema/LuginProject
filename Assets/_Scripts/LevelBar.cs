using System;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
    [SerializeField] float experienceRequiredMultiplayer;
    [SerializeField] float[] experienceRequiredToLevelUp;

    private int level;
    private float experienceLeftOver;
    private float currentExperience;
    private float currentExperienceRequiredToLevelUp;

    public event Action OnLevelUp;

    private void Start()
    {
        if(experienceRequiredToLevelUp.Length <= 0)
        {
            Debug.LogError("Add at least one element in the array to have the first target to level up", this);
            return;
        }

        currentExperienceRequiredToLevelUp = experienceRequiredToLevelUp[0];
    }

    public void AddExp(float amount)
    {
        currentExperience += amount;
        /* TODO - Update UI
         * Normalize the value and take the min[ Mathf.Min(currentExperience / currentExperienceRequiredToLevelUp, currentExperienceRequiredToLevelUp) ]
         */

        if (currentExperience >= currentExperienceRequiredToLevelUp)
        {
            experienceLeftOver = currentExperience - currentExperienceRequiredToLevelUp;
            level++;

            if(level < experienceRequiredToLevelUp.Length - 1)
            {
                currentExperienceRequiredToLevelUp = experienceRequiredToLevelUp[level];
            }
            else
            {
                currentExperienceRequiredToLevelUp *= experienceRequiredMultiplayer;
            }

            OnLevelUp?.Invoke();
        }
    }

}
