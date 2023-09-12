using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class LevelCounter : MonoBehaviour
{
    public static LevelCounter Instance;

    public int level;
    public int levelType;

    List<int> firstLevelsList;
    List<int> secondLevelsList;
    List<int> thirdLevelsList;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(this.gameObject);
    }   

    void Start(){
        int[] firstLevels = { 0, 1, 2 };
        int[] secondLevels = { 3, 4, 5 };
        int[] thirdLevels = { 6, 7, 8, 9 };

        firstLevelsList = firstLevels.ToList();
        secondLevelsList = secondLevels.ToList();
        thirdLevelsList = thirdLevels.ToList();
    }



    public int GetLevelType(){
        if(level < 3){
            int index = Random.Range(0, firstLevelsList.Count);
            levelType = firstLevelsList[index];
            firstLevelsList.Remove(levelType);
        }
        else if(level < 6){
            int index = Random.Range(0, secondLevelsList.Count);
            levelType = secondLevelsList[index];
            secondLevelsList.Remove(levelType);
        }
        else{
            int index = Random.Range(0, thirdLevelsList.Count);
            levelType = thirdLevelsList[index];
            thirdLevelsList.Remove(levelType);
        }      
        Debug.Log("Gelen level: " + levelType);  
        return levelType;
    }
    public void IncreaseLevel(){
        level++;
    } 
}
