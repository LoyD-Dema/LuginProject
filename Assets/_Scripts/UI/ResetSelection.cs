using UnityEngine;
using UnityEngine.EventSystems;

public class ResetSelection : MonoBehaviour
{
    private GameObject lastSelectedElement;

    private void Start()
    {
        lastSelectedElement = EventSystem.current.currentSelectedGameObject;
    }

    private void Update()
    {
        GameObject currentSelectedElement = EventSystem.current.currentSelectedGameObject;
        if (currentSelectedElement == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedElement);
        }
        else if (currentSelectedElement != lastSelectedElement)
        {
            lastSelectedElement = currentSelectedElement;
        }
    }
}
