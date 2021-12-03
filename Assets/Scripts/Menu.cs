using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Menu : MonoBehaviour
{
    
    private int bScore = Data.data.bestScore;
    private string bGamer = Data.data.bestGamer;
    public Text BestScore;
    void Start()
    {
        AddBestText();
    }

    void AddBestText()
    {
        BestScore.text = "Best Score: "+bGamer+" : "+bScore;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Data.data.SaveBests();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SelectName(string s)
    {
        Data.data.nameRead = s;
        //Debug.Log(Data.data.nameRead);

      
    }

}
