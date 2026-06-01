using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 Un helper per i bottoni in modo da poter gestire l'hover del mouse
 */

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler

{
    private MenuNavigation menuNavigation;
    private Button button;

    //Ottengo i riferimenti
    private void Awake()
    {
        button = GetComponent<Button>();
        menuNavigation = GetComponentInParent<MenuNavigation>();
    }

    //Interfaccia implementata per dire su quale bottone sto facendo Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!menuNavigation && !button) return;

        menuNavigation.HandlePointerEnter(button);
    }
}
