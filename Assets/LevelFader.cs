using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelFader : MonoBehaviour
{

    public Animator animator;

    private int levelToLoad;
    // Update is called once per frame

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    
}
