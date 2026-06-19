using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveObjectNextToSelectedUIObject : MonoBehaviour
{
    [SerializeField] MoveingUIElement[] elements;
    private Vector2[] targetPos;

    private Canvas canvas;
    private RectTransform canvasRectTransform;

    [SerializeField] ResetSelection resetSelection;


    [SerializeField] float moveSpeed = 5.0f;

    private bool canMove;

    private void Awake()
    {
        if (TryGetComponent<Canvas>(out canvas))
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        targetPos = new Vector2[elements.Length];

        for (int i = 0; i < elements.Length; i++)
        {
            if (!elements[i].RectTransform) continue;
            elements[i].SetImage();
        }
    }

    private void OnEnable()
    {
        if (elements.Length <= 0 || canvasRectTransform == null)
            return;

        resetSelection.OnSelectedElementChange += ResetSelection_OnSelectedElementChange;
    }

    private void Start()
    {
        //Forziamo il reset della canvas nel primo frame
        Canvas.ForceUpdateCanvases();

        ResetSelection_OnSelectedElementChange();

        //Settiamo senza lerp la posizione delle frecce appena si va in play
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].RectTransform.anchoredPosition = targetPos[i];
            elements[i].SetSprite(elements[i].NormalSprite);
        }
        canMove = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        resetSelection.OnSelectedElementChange -= ResetSelection_OnSelectedElementChange;
    }

    private void ResetSelection_OnSelectedElementChange()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        AudioManager.PlaySound2D(SoundType.HoverButton);

        RectTransform sObjectRectTransform = selectedObject.GetComponent<RectTransform>();
        if (!sObjectRectTransform) return;

        canMove = true;

        Vector2 elementPos = SimulateReparentAndReanchor(sObjectRectTransform);

        Debug.Log(elementPos);

        float left = elementPos.x - (sObjectRectTransform.rect.width * sObjectRectTransform.pivot.x);
        float right = elementPos.x + (sObjectRectTransform.rect.width * (1 - sObjectRectTransform.pivot.x));

        for (int i = 0; i < targetPos.Length; i++)
        {
            float yPos = elementPos.y + elements[i].YOffset;
            float xPos = elements[i].IsXPositive ? right + elements[i].XOffset : left - elements[i].XOffset;

            targetPos[i] = new Vector2(xPos, yPos);

            elements[i].SetSprite(elements[i].NormalSprite);
        }
    }

    public Vector2 SimulateReparentAndReanchor(RectTransform element)
    {
        Vector2 centeredAncors = new Vector2(0.5f, 0.5f);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, element.position, null, out Vector2 localPoint);
        return localPoint - centeredAncors;
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
        int elemetsReached = 0;

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].RectTransform.anchoredPosition = Vector2.Lerp(elements[i].RectTransform.anchoredPosition, targetPos[i], moveSpeed * Time.unscaledDeltaTime);

            if ((elements[i].RectTransform.anchoredPosition - targetPos[i]).magnitude < 0.05f)
            {
                elements[i].RectTransform.anchoredPosition = targetPos[i];
                elements[i].SetSprite(elements[i].NormalSprite);
                elemetsReached++;
            }
        }

        if (elemetsReached == elements.Length)
        {
            canMove = false;
        }
    }

    public void UpdateArrow()
    {
        AudioManager.PlaySound2D(SoundType.SelectionButton);

        //Per evitare che si blocchi la freccia nello stato di pressed
        StopAllCoroutines();

        //Inizia la coroutine per aggiornare le frecce
        StartCoroutine(PressAndResetArrow());
    }

    private IEnumerator PressAndResetArrow()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetSprite(elements[i].PressedSprite);
        }

        yield return new WaitForSecondsRealtime(0.1f);

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetSprite(elements[i].NormalSprite);
        }
    }
}

[Serializable]
public struct MoveingUIElement
{
    public RectTransform RectTransform;
    public bool IsXPositive;
    public float XOffset;
    public float YOffset;

    [Header("Sprite Images")]
    public Sprite NormalSprite;
    public Sprite PressedSprite;

    private Image image;

    public void SetImage()
    {
        image = RectTransform.GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        if (!image && !sprite) return;
        image.sprite = sprite;
    }
}
