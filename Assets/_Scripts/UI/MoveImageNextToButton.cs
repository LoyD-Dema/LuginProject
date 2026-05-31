using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveImageNextToButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    private Button button;
    [SerializeField] Image[] images;
    private Vector3[] targetPos;
    [SerializeField] float moveSpeed = 5.0f;

    private bool canMove;


    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        targetPos = new Vector3[images.Length];
    }

    public void OnSelect(BaseEventData eventData)
    {
        canMove = true;
        for (int i = 0; i < images.Length; i++)
        {
            targetPos[i] = new Vector3(images[i].transform.position.x, button.transform.position.y, images[i].transform.position.z);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        canMove = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.Select();
    }

    private void Update()
    {
        if (canMove)
        {
            MoveArrows();
        }
    }

    private void MoveArrows()
    {
        bool canStopMove = false;

        for (int i = 0; i < images.Length; i++)
        {
            Debug.Log(images[i].transform.position + " - " + targetPos[i]);

            images[i].transform.position = Vector3.Lerp(images[i].transform.position, targetPos[i], moveSpeed * Time.deltaTime);

            if((images[i].transform.position - targetPos[i]).magnitude < 0.05f)
            {
                images[i].transform.position = targetPos[i];
                canStopMove = true;
                Debug.Log("Reached");
            }
        }

        if(canStopMove)
        {
            canMove = false;
        }
    }

   
}
