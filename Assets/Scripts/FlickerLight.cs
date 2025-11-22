using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    Light2D light2D;
    float baseIntensity;

    [SerializeField] public float minIntensity = -0.05f;
    [SerializeField] public float maxIntensity = 0.05f;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        baseIntensity = light2D.intensity;
    }

    void Update()
    {
        light2D.intensity = baseIntensity + Random.Range(minIntensity, maxIntensity);
    }
}
