using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference music { get; private set; }
    [field: SerializeField] public EventReference ambience { get; private set; }
    [field: SerializeField] public EventReference buttonSFX { get; private set; }
    [field: SerializeField] public EventReference swingMissSFX { get; private set; }
    [field: SerializeField] public EventReference attackSFX { get; private set; }
    [field: SerializeField] public EventReference healSFX { get; private set; }
    [field: SerializeField] public EventReference itemSFX { get; private set; }
    [field: SerializeField] public EventReference deathSFX { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Found more than one FMODEvents, destroying the new one.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}