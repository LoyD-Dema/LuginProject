using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private bool bIsClicking = false;

    //Setto Index a 0 e aggiorno la canvas
    private void OnEnable()
    {
        if (buttons.Length == 0 || !arrowVisual) return;

        currentIndex = 0;
        UpdateMenuVisual();
    }
    #region Navigation
    //Ricevo i comandi dall'InputSystem con W vado verso l'alto con S verso il basso nel menu
    public void OnNavigate(InputValue value)
    {
        if (buttons.Length == 0) return;

        Vector2 direction = value.Get<Vector2>();
        if (direction.y < -0.1f)
        {
            currentIndex = (currentIndex + 1) % buttons.Length;
            UpdateMenuVisual();
        }
        else if (direction.y > 0.1f)
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
            UpdateMenuVisual();
        }
    }

    //Tramite InputSystem se il valore ì true chiamo La conferma di selezione
    public void OnSubmit(InputValue value)
    {
        if (value.isPressed)
        {
            ConfirmSelection();
        }
    }

    //chiamo la Coroutine per l'animazione del bottone
    private void ConfirmSelection()
    {
        if (buttons == null) return;

        StartCoroutine(ClickCoroutine());
    }

    //viene chiamata dai bottoni quando ci si passa sopra
    //se il bottone selezionato è differente da quello precedente setto l'indice
    public void HandlePointerEnter(Button button)
    {
        if (bIsClicking) return;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == button)
            {
                if (currentIndex != i)
                {
                    currentIndex = i;
                    UpdateMenuVisual();
                }
            }
        }
    }
#endregion
    #region Visual Update

    //Setto la posizione delle frecce laterali sul bottone
    //Il ciclo è per forzare lo stato dei bottoni e applicare una leggera sfumatura
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
    #endregion

    #region Coroutine
    //coroutine per cambiare lo sprite del bottone e delle frecce
    //finita la coroutine viene invocato il funzionamento dei bottoni
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
    #endregion
}
