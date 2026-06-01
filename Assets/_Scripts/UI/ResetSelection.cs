using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetSelection : MonoBehaviour
{
    private GameObject lastSelectedElement;
    public event Action OnSelectedElementChange;
    [SerializeField] GameObject firstSelected;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
        lastSelectedElement = EventSystem.current.currentSelectedGameObject;
        OnSelectedElementChange?.Invoke();
    }

    private void Update()
    {
        GameObject currentSelectedElement = EventSystem.current.currentSelectedGameObject;
        if (currentSelectedElement == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedElement);
            OnSelectedElementChange?.Invoke();
        }
        else if (currentSelectedElement != lastSelectedElement)
        {
            lastSelectedElement = currentSelectedElement;
            OnSelectedElementChange?.Invoke();
        }
    }
}
