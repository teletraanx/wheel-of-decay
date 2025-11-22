using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Light2DPulse : MonoBehaviour
{
    [SerializeField] public float minIntensity = 0.5f;
    [SerializeField] public float maxIntensity = 3f;
    [SerializeField] public float pulseSpeed = 2f;

    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}
