using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            //OnSelectedElementChange?.Invoke();
        }
        else if (currentSelectedElement != lastSelectedElement)
        {
            lastSelectedElement = currentSelectedElement;
            OnSelectedElementChange?.Invoke();
        }
    }

    public void ForceSelection(Selectable selectableObj)
    {
        EventSystem.current.SetSelectedGameObject(selectableObj.gameObject);
        //Debug.Break();
        //Debug.Log(lastSelectedElement.gameObject.name);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}
