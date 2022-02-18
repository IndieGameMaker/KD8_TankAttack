using UnityEngine;
using UnityEngine.EventSystems;

public class IgnoreUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool isMouseOverUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOverUI = false;
    }
}
