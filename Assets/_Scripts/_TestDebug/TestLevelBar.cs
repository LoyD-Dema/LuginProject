using UnityEngine;

public class TestLevelBar : MonoBehaviour
{
    [SerializeField] private LevelBar levelBar;
    [SerializeField] private float expToAdd;
    [SerializeField] private bool enableDebugMessage = true;

    private void OnEnable()
    {
        levelBar.OnLevelUp += LevelBar_OnLevelUp;
    }

    private void OnDisable()
    {
        levelBar.OnLevelUp -= LevelBar_OnLevelUp;
    }

    private void LevelBar_OnLevelUp()
    {
        if(enableDebugMessage)
        {
            Debug.Log("LevelUpDone, current level: " + levelBar.Level);
        }
    }

    // Called when space is pressed
    private void OnJump()
    {
        levelBar.AddExp(expToAdd);
    }
}
