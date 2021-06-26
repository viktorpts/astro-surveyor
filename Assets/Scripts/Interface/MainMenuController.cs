﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator overlay;


    public void StartTutorial()
    {
        Fade(false);
        StartCoroutine(ChangeScene("DemoLevel"));
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