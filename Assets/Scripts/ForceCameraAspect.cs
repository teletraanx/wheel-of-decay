using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ForceCameraAspect : MonoBehaviour
{
    public float targetAspect = 16f / 10f;  // 1.6

    void Update()
    {
        Camera cam = GetComponent<Camera>();

        float windowAspect = (float)Screen.width / Screen.height;
        float scale = windowAspect / targetAspect;

        if (scale < 1f)
        {
            // Letterbox (bars top/bottom)
            cam.rect = new Rect(0, (1f - scale) / 2f, 1f, scale);
        }
        else
        {
            // Pillarbox (bars left/right)
            float scaleWidth = 1f / scale;
            cam.rect = new Rect((1f - scaleWidth) / 2f, 0, scaleWidth, 1f);
        }
    }
}
