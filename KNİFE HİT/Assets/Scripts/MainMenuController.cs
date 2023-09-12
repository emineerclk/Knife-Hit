using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void playGame()
    {
        if(Health.Instance != null)
            Health.Instance.SetHealth(3);
        
        if(ScoreManager.Instance != null)
            ScoreManager.Instance.SetScore(0);
        
        SceneManager.LoadScene(1);

    }
    public void mainMenu()
    {
        SceneManager.LoadScene("Main menu");
           
    }
    public void settingsMenu()
    {
        SceneManager.LoadScene("Again");
    }
    public void quitGame()
    {
        Application.Quit();
    } 

}

