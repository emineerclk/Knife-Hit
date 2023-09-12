using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;

    public Text Scoretext;

    void Awake()
    {
        //score = PlayerPrefs.GetInt("score");

        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(this.gameObject);
    }

    void Start() 
    {
        Scoretext.text = score.ToString();
    }

    public void IncreaseScore()
    {
        score++;
        Scoretext.text = score.ToString();
        //PlayerPrefs.SetInt("score", score);
    }

    public void SetScore(int num) {
        score = num;
        Scoretext.text = score.ToString();
    }
}
