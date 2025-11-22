using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] public string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
