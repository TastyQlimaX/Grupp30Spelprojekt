using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public LevelFader _levelFader;
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _levelFader.FadeToLevel(1);
    }

    public void QuitGame()
    {
        Debug.Log("Game will quit in finished build");
        Application.Quit();
    }
}
