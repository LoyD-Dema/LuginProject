using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOnHoover : MonoBehaviour, IPointerEnterHandler
{
    private Selectable button;

    private void Awake()
    {
        button = GetComponent<Selectable>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.Select();
    }  
}
