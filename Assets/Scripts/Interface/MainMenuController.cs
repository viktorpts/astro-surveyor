using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator overlay;


    public void StartTutorial()
    {
        Fade(false);
        StartCoroutine(ChangeScene("Tutorial"));
    }

    public void StartLevel1()
    {
        Fade(false);
        StartCoroutine(ChangeScene("Level1"));
    }

    public void StartLevel2()
    {
        Fade(false);
        StartCoroutine(ChangeScene("Level2"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene(string scene) {

        var p = SceneManager.LoadSceneAsync(scene);
        while(!p.isDone) {
            yield return null;
        }
    }

    void Fade(bool fadeIn)
    {
        overlay.SetBool("fadeIn", fadeIn);
    }
}
