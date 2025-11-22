using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TooltipManager : MonoBehaviour
{
    private static TooltipManager _instance;
    public static TooltipManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Unity 6+ way: look even through inactive objects
                _instance = Object.FindFirstObjectByType<TooltipManager>(FindObjectsInactive.Include);
            }
            return _instance;
        }
    }

    [Header("Tooltip UI Elements")]
    public TMP_Text tooltipText;
    public RectTransform backgroundRect;
    public Canvas canvas;

    void Awake()
    {
        _instance = this;
        gameObject.SetActive(false); // start hidden
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        transform.localPosition = localPoint + new Vector2(60, -30);
    }

    public void Show(string description)
    {
        if (tooltipText == null) return;
        tooltipText.text = description;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (this != null)
            gameObject.SetActive(false);
    }
}
