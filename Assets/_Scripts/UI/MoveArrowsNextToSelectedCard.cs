using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveArrowsNextToSelectedCard : MonoBehaviour
{
    [SerializeField] private MovingCardUIElement[] arrowElements;
    private Vector2[] targetPositions;

    private Canvas canvas;
    private RectTransform canvasRectTransform;

    [SerializeField] private ResetSelection resetSelection;
    [SerializeField] private float moveSpeed = 5.0f;

    private bool canMove;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        canvasRectTransform = canvas.GetComponent<RectTransform>();

        targetPositions = new Vector2[arrowElements.Length];

        for (int i = 0; i < arrowElements.Length; i++)
        {
            if (!arrowElements[i].RectTransform) continue;
            arrowElements[i].SetImage();
        }
    }

    private void OnEnable()
    {
        if (arrowElements.Length <= 0 || canvasRectTransform == null || resetSelection == null)
            return;

        resetSelection.OnSelectedElementChange += HandleSelectedCardChanged;
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();

        HandleSelectedCardChanged();

        for (int i = 0; i < arrowElements.Length; i++)
        {
            if (!arrowElements[i].RectTransform) continue;
            arrowElements[i].RectTransform.anchoredPosition = targetPositions[i];
            arrowElements[i].SetSprite(arrowElements[i].NormalSprite);
        }
        canMove = false;
    }

    private void OnDisable()
    {
        resetSelection.OnSelectedElementChange -= HandleSelectedCardChanged;
    }

    private void HandleSelectedCardChanged()
    {
        if (EventSystem.current.currentSelectedGameObject == null) return;


        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        RectTransform selectedCardRect = selectedObject.GetComponent<RectTransform>();
        if (!selectedCardRect) return;

        canMove = true;

        Vector2 cardLocalPos = SimulateReparentAndReanchor(selectedCardRect);

        float left = cardLocalPos.x - (selectedCardRect.rect.width * selectedCardRect.pivot.x);
        float right = cardLocalPos.x + (selectedCardRect.rect.width * (1 - selectedCardRect.pivot.x));

        for (int i = 0; i < targetPositions.Length; i++)
        {
            float yPos = cardLocalPos.y + arrowElements[i].YOffset;

            float xPos = arrowElements[i].IsXPositive ?
                right + arrowElements[i].XOffset :
                left - arrowElements[i].XOffset;

            targetPositions[i] = new Vector2(xPos, yPos);

            arrowElements[i].SetSprite(arrowElements[i].NormalSprite);
        }
    }
    private Vector2 SimulateReparentAndReanchor(RectTransform element)
    {
        Vector2 centeredAnchors = new Vector2(0.5f, 0.5f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, element.position, null, out Vector2 localPoint);
        return localPoint - centeredAnchors;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        int elementsReached = 0;

        for (int i = 0; i < arrowElements.Length; i++)
        {
            arrowElements[i].RectTransform.anchoredPosition = Vector2.Lerp(arrowElements[i].RectTransform.anchoredPosition, targetPositions[i], moveSpeed * Time.unscaledDeltaTime);

            if ((arrowElements[i].RectTransform.anchoredPosition - targetPositions[i]).magnitude < 0.05f)
            {
                arrowElements[i].RectTransform.anchoredPosition = targetPositions[i];
                arrowElements[i].SetSprite(arrowElements[i].NormalSprite);
                elementsReached++;
            }
        }

        if (elementsReached == arrowElements.Length)
        {
            canMove = false;
        }
    }

    // Feedback visivo alla pressione del tasto di conferma (Submit/Invio)
    public void TriggerArrowPressFeedback()
    {
        StopAllCoroutines();
        StartCoroutine(PressAndResetArrowSequence());
    }

    private IEnumerator PressAndResetArrowSequence()
    {
        for (int i = 0; i < arrowElements.Length; i++)
        {
            arrowElements[i].SetSprite(arrowElements[i].PressedSprite);
        }

        yield return new WaitForSecondsRealtime(0.1f);

        for (int i = 0; i < arrowElements.Length; i++)
        {
            arrowElements[i].SetSprite(arrowElements[i].NormalSprite);
        }
    }
}

[Serializable]
public struct MovingCardUIElement
{
    public RectTransform RectTransform;
    public bool IsXPositive; // True = Destra della carta, False = Sinistra della carta
    public float XOffset;
    public float YOffset;

    [Header("Sprites")]
    public Sprite NormalSprite;
    public Sprite PressedSprite;

    private Image image;

    public void SetImage()
    {
        image = RectTransform.GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        if (image != null && sprite != null)
        {
            image.sprite = sprite;
        }
    }
}