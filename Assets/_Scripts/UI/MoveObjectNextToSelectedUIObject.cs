using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if(TryGetComponent<Canvas>(out canvas))
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        targetPos = new Vector2[elements.Length];
    }

    private void OnEnable()
    {
        if (elements.Length <= 0 || canvasRectTransform == null)
            return;
        
        resetSelection.OnSelectedElementChange += ResetSelection_OnSelectedElementChange;
    }

    private void OnDisable()
    {
        resetSelection.OnSelectedElementChange -= ResetSelection_OnSelectedElementChange;
    }

    private void ResetSelection_OnSelectedElementChange()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        RectTransform sObjectRectTransform = selectedObject.GetComponent<RectTransform>();
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
            elements[i].RectTransform.anchoredPosition = Vector2.Lerp(elements[i].RectTransform.anchoredPosition, targetPos[i], moveSpeed * Time.deltaTime);

            if ((elements[i].RectTransform.anchoredPosition - targetPos[i]).magnitude < 0.05f)
            {
                elements[i].RectTransform.anchoredPosition = targetPos[i];
                elemetsReached++;
            }
        }

        if (elemetsReached == elements.Length)
        {
            canMove = false;
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
}
