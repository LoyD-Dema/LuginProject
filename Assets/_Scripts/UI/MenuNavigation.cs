using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] MenuVisualArrow arrowVisual;
    [SerializeField] Button[] buttons;

    [Header("Sprite")]
    [SerializeField] Sprite pressedButtonSprite;
    [SerializeField] float clickDelay = 0.2f;

    private int currentIndex = 0;
    private bool bIsClicking;

    private void OnEnable()
    {
        if (buttons.Length == 0 ||!arrowVisual) return;

        currentIndex = 0;
        Canvas.ForceUpdateCanvases();
        UpdateMenuVisual();
    }

    public void OnNavigate(InputValue value)
    {
        if (buttons.Length == 0) return;
        
        Vector2 direction = value.Get<Vector2>();
        if (direction.y < -0.1f)
        {
            currentIndex = (currentIndex + 1) % buttons.Length;
            UpdateMenuVisual();
        }
        else if(direction.y > 0.1f)
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
            UpdateMenuVisual();
        }
    }

    public void OnSubmit(InputValue value)
    {
        if (value.isPressed)
        {
            ConfirmSelection();
        }
    }

    private void UpdateMenuVisual()
    {
        RectTransform currentButton = buttons[currentIndex].GetComponent<RectTransform>();

        arrowVisual.MoveArrowsToTarget(currentButton);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == currentIndex)
            {
                buttons[i].OnSelect(null);
            }
            else
            {
                buttons[i].OnDeselect(null);
            }
        }
    }

    private void ConfirmSelection()
    {
        if (buttons == null) return;

        StartCoroutine(ClickCoroutine());
    }

    private IEnumerator ClickCoroutine()
    {
        bIsClicking = true;

        Button currentButton = buttons[currentIndex];
        Image buttonImage = currentButton.GetComponent<Image>();
        Sprite normalButtonSprite = (buttonImage != null) ? buttonImage.sprite : null;

        if (arrowVisual) arrowVisual.SetPressedState(true);
        if (buttonImage && pressedButtonSprite) buttonImage.sprite = pressedButtonSprite; ;

        yield return new WaitForSeconds(clickDelay);

        if (arrowVisual) arrowVisual.SetPressedState(false);
        if (buttonImage && pressedButtonSprite) buttonImage.sprite = normalButtonSprite;

        bIsClicking = false;
        currentButton.onClick.Invoke();

    }
}
