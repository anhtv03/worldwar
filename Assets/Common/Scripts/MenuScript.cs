using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //[SerializeField] GameObject PausePanel;
    [SerializeField] int numberScence = 0;

    //  1: Shank
    //  2: Bibi Xuan Mai
    //  3: Joe Higashi
    //  4: Yugi
    static int play1 = 0;
    static int play2 = 0;
    static int background = 0;

    public void MoveSence()
    {
        StartCoroutine(Wait());
        numberScence++;

        if(numberScence == 4)
        {
            PlayerPrefs.SetInt("play1", play1);
            PlayerPrefs.SetInt("play2", play2);
            PlayerPrefs.SetInt("background", background);
        }

        SceneManager.LoadScene(numberScence);
        Time.timeScale = 1;
    }

    public void BackScene() {
        StartCoroutine(Wait());
        numberScence--;
        SceneManager.LoadScene(numberScence);
        Time.timeScale = 1;
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(1);
    }
    public void Exit()
    {
        Application.Quit();
    }


    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ChoosePlayer1(int numberPlayer)
    {
        play1 = numberPlayer;
        MoveSence();
    }

    public void ChoosePlayer2(int numberPlayer)
    {
        play2 = numberPlayer;
        MoveSence();
    }

    public void ChooseBackground(int numberBackground)
    {
        background = numberBackground;
        MoveSence();
    }

    public void MoveHome()
    {
        SceneManager.LoadScene(0);
    }

}
