using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int point;
    public Text highscore;
    
    void Start()
    {
        point = PlayerPrefs.GetInt("highscore");
        highscore.text = "Highest Score = " + point;
    }

    
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("1");
    }
  
    public void About()
    {
        SceneManager.LoadScene("About");
    }
    public void Quit()
    {
        Application.Quit();
    }


}
