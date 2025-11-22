using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{
    string gameSceneName = "SampleScene";
    string endSceneName = "EndingScene";
    string openingSceneName = "OpeningScene";
    string menuSceneName = "MenuScene";
    string creditSceneName = "CreditScene";

    public void GoToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopCaveAmbience();
            AudioManager.instance.SetMusicParameter("Area", 0.0f);
        }
    }

    public void GoToEndScene()
    {
        SceneManager.LoadScene(endSceneName);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopCaveAmbience();
            AudioManager.instance.SetMusicParameter("Area", 0.0f);
        }
    }

    public void GoToOpeningScene()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        SceneManager.LoadScene(openingSceneName);
    }

    public void GoToCreditScene()
    {
        SceneManager.LoadScene(creditSceneName);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
