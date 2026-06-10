using UnityEngine;
using UnityEngine.UI;

public class MenuVisualArrow : MonoBehaviour
{
    [Header("Arrows")]
    [SerializeField] RectTransform leftArrowRect;
    [SerializeField] RectTransform rightArrowRect;
    [SerializeField] Image leftArrowImage;
    [SerializeField] Image rightArrowImage;

    [Header("Sprites")]
    [SerializeField] Sprite leftNormalSprite;
    [SerializeField] Sprite rightNormalSprite;
    [SerializeField] Sprite leftPressedSprite;
    [SerializeField] Sprite rightPressedSprite;

    [Header("Positions")]
    [SerializeField] float padding = 45f;

    //ricevo un RectTransform e setto la posizione delle frecce
    public void MoveArrowsToTarget(RectTransform target)
    {
        if (!target || !leftArrowRect ||  !rightArrowRect) return;

        Vector3 targetPos = target.localPosition;

        float targetHalfWidth = target.rect.width * 0.5f;

        leftArrowRect.localPosition = new Vector3(targetPos.x - targetHalfWidth - padding, targetPos.y, targetPos.z);
        rightArrowRect.localPosition = new Vector3(targetPos.x + targetHalfWidth + padding, targetPos.y, targetPos.z);
    }
    //update visivo delle frecce quando si effettua una selezione
    public void SetPressedState(bool bIsPressed)
    {
        if (!leftArrowImage || !rightArrowImage) return;

        if (bIsPressed)
        {
            if (leftPressedSprite) leftArrowImage.sprite = leftPressedSprite;
            if (rightPressedSprite) rightArrowImage.sprite = rightPressedSprite;
        }
        else
        {
            if (leftNormalSprite) leftArrowImage.sprite = leftNormalSprite;
            if (rightNormalSprite) rightArrowImage.sprite = rightNormalSprite;
        }
    }

}
