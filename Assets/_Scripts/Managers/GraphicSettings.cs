using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Button applyButton;
    private bool toggleValue; 
    private bool currentValue;

    private void Start()
    {
        applyButton.gameObject.SetActive(false);
        bool isFullScreen = PlayerPrefs.GetInt("fullScreen", 1) == 1;
        fullScreenToggle.isOn = isFullScreen;
        currentValue = isFullScreen;
        toggleValue = currentValue;
        Apply();
    }

    public void ChangeValue(bool value)
    {
        toggleValue = value;

        if(currentValue != toggleValue)
        {
            applyButton.gameObject.SetActive(true);
        }
        else
        {
            applyButton.gameObject.SetActive(false);
        }
    }

    public void Apply()
    {
        Screen.fullScreen = toggleValue;
        currentValue = toggleValue;
        applyButton.gameObject.SetActive(false);

        if (currentValue)
        {
            PlayerPrefs.SetInt("fullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullScreen", 0);
        }
    }
}
