using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;
    private EventInstance ambientEventInstance;

    private bool initialized = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Debug.LogError("Found more than one Audio Manager in the scene.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!initialized)
        {
            InitializeMusic(FMODEvents.instance.music);
            initialized = true;
        }
    }

    private void InitializeMusic(EventReference musicEventReference)
    {

        musicEventInstance = RuntimeManager.CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetMusicParameter(string parameterName, float value)
    {
        if (!musicEventInstance.isValid())
        {
            Debug.LogWarning("Tried to set music parameter, but musicEventInstance is not valid yet.");
            return;
        }

        var result = musicEventInstance.setParameterByName(parameterName, value);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError($"FMOD: Failed to set EVENT parameter {parameterName}: {result}");
        }
    }

    public void PlayCaveAmbience()
    {
        // Create it only once
        if (!ambientEventInstance.isValid())
        {
            ambientEventInstance = RuntimeManager.CreateInstance(FMODEvents.instance.ambience);
            ambientEventInstance.start();
        }
    }

    public void StopCaveAmbience(bool fadeOut = true)
    {
        if (!ambientEventInstance.isValid())
            return;

        var mode = fadeOut ? FMOD.Studio.STOP_MODE.ALLOWFADEOUT
                           : FMOD.Studio.STOP_MODE.IMMEDIATE;

        ambientEventInstance.stop(mode);
        ambientEventInstance.release();
        ambientEventInstance.clearHandle();
    }

}
