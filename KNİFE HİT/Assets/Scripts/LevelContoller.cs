using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Random;

public class LevelContoller : MonoBehaviour
{
    public static LevelContoller Instance;

    private int levelNumber = 0;
    public int knifeCount = 0;

    public int thrownKnife = 0;

    [SerializeField] HolderActivater holderActivater;
    
    
    public int atilanBicak = 0;

    [SerializeField] private float WatermelonHizi;
    [SerializeField] private float WatermelonMinHizi;
    [SerializeField] private float WatermelonMaxHizi;
    [SerializeField] private float WatermelonMinHizi1;

    [SerializeField] private float WatermelonMaxHizi2;
    [SerializeField] private float WatermelonMinHizi3;
    [SerializeField] private float WatermelonMaxHizi4;


    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite limeSprite;

    public int ToplamLevelTuru = 10;

    private bool isPaused = false;
    private float pauseDuration = 0.1f;
    private float pauseTimer = 0f;
    private float moveDuration = 0.01f;
    private float moveTimer = 0f;
    private int passedLevelCount;

    [Header("Watermelan Speed Controllers")]
    float _minSpeedController, _maxSpeedController;


    int levelTuru;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        levelTuru = LevelCounter.Instance.GetLevelType();
        SetMinMaxSpeed(levelTuru);

        if (levelTuru<3){
            knifeCount=4;
        } 
        else if (levelTuru<6){
            knifeCount=6;
        }
       else {
        knifeCount=8;
       }

        SetRandomknifeCount();

        // BU İKİ FONKSİYON AYNI İŞLEVİ GÖRÜYO. BİRİNİN ELİMİNE EDİLMESİ LAZIM
        //SetRandomWatermelonHizi();
        SetLevelProperties();


        holderActivater.ActivateHolders(knifeCount);
    }

    public void IncreaselevelNumber()
    {
        levelNumber++;
    }

    public int GetlevelNumber()
    {
        return levelNumber;
    }

    public void IncreaseknifeCount()

    {
        knifeCount++;
    }

    public bool CheckGameOver()
    {
        if (knifeCount == atilanBicak)
            return true;
        else
            return false;
    }
    public int GetknifeNumber()
    {
        return knifeCount;
    }
   

    public void SetMinknifeCount(int mincount)
    {
        knifeCount = mincount;
    }

   

    

    public void SetRandomknifeCount()
    {
        
        Watermelon.Instance.SetToplamBicak(knifeCount);
    }

    public float GetWatermelonHizi()
    {
        return WatermelonHizi;
    }

    public float GetWatermelonMinHizi()
    {
        return WatermelonMinHizi;
    }

    public float GetWatermelonMaxHizi()
    {
        return WatermelonMaxHizi;
    }

    public void SetRandomWatermelonHizi()
    {
        // WatermelonHizi = Random.Range(WatermelonMinHizi, WatermelonMaxHizi);
        // Watermelon.Instance.rotationSpeed = WatermelonHizi;
    }
    public void SetLevelProperties()
    {

        Watermelon.Instance.levelCase = levelTuru;


        switch (levelTuru)
        {
            case 0:
                Debug.Log("0000");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.rotationDirection = 1;
                break;

            case 1:
                Debug.Log("1111");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.rotationDirection = -1;
                break;

            case 2:
                Debug.Log("2222");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.rotationDirection = 1;
                Watermelon.Instance.GetComponent<SpriteRenderer>().sprite = orangeSprite;
                Watermelon.Instance.ChangeFruitParts(1);
                // isPaused = false;
                // pauseTimer = 0f;
                // Watermelon.Instance.rotationDirection = 1;
                break;

            case 3:
                Debug.Log("3333");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController); ;
                Watermelon.Instance.rotationDirection = 1;
                isPaused = false;
                pauseTimer = 0f;
                Watermelon.Instance.rotationDirection = -1;
                break;

            case 4:
                Debug.Log("4444");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController); ;
                Watermelon.Instance.rotationDirection = 1;
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveDuration)
                {
                    moveTimer = 0f;
                    isPaused = !isPaused;
                }

                if (isPaused)
                {
                    pauseTimer += Time.deltaTime;
                    if (pauseTimer >= 0.01f)
                    {
                        pauseTimer = 0f;
                    }
                }
                break;

            case 5:
                Debug.Log("55555");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.rotationDirection = 1;

                Watermelon.Instance.GetComponent<SpriteRenderer>().sprite = orangeSprite;
                Watermelon.Instance.ChangeFruitParts(1);
                Watermelon.Instance.rotationDirection = 1;
                break;

            case 6:
                Debug.Log("6666");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.ChangeFruitParts(2);
                Watermelon.Instance.ChangeSprite(limeSprite);
                Watermelon.Instance.rotationDirection = 1;
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveDuration)
                {
                    moveTimer = 0f;
                    Watermelon.Instance.rotationDirection = -1;
                }
                break;

            case 7:
                Debug.Log("7777");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.rotationDirection = -1;
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveDuration)
                {
                    moveTimer = 0f;
                    isPaused = !isPaused;
                }

                if (isPaused)
                {
                    pauseTimer += Time.deltaTime;
                    if (pauseTimer >= 0.01f)
                    {
                        pauseTimer = 0f;
                    }
                    Watermelon.Instance.rotationDirection = 1;
                }
                break;

            case 8:
                Debug.Log("8888");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.ChangeFruitParts(2);
                Watermelon.Instance.ChangeSprite(limeSprite);
                break;
            case 9:
                Debug.Log("9999");
                WatermelonHizi = Random.Range(_minSpeedController, _maxSpeedController);
                Watermelon.Instance.rotationDirection = 1;
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveDuration)
                {
                    moveTimer = 0f;
                    Watermelon.Instance.rotationDirection = -1;
                }
                break;

        }
        Watermelon.Instance.rotationSpeed = WatermelonHizi;
        Watermelon.Instance.SetRandomWatermelonHizi = true;
        levelNumber++;

    }

    public void SetMinMaxSpeed(int num) {
        if(num < 3) {
            _minSpeedController = WatermelonMinHizi;
            _maxSpeedController = WatermelonMaxHizi;
        }
        else if(num < 6) {
            _minSpeedController = WatermelonMinHizi1;
            _maxSpeedController = WatermelonMaxHizi2;
        }
        else {
            _minSpeedController = WatermelonMinHizi3;
            _maxSpeedController = WatermelonMaxHizi4;
        }

        Debug.Log("Min: " + _minSpeedController);
        Debug.Log("Max: " + _maxSpeedController);
    }

    public void LevelComplete()
    {
        LevelCounter.Instance.IncreaseLevel();

        
        // Önce, mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Sonra "LoadNextLevel" işlevini çağırarak bir sonraki seviyeye geç
        LoadNextLevel();
    }
    public void LoadNextLevel()
    {

        levelNumber++;

        Debug.Log("gakgaajkaegejgjejejgejgejjgje");


        if (levelNumber > ToplamLevelTuru)
        {
            Debug.Log("Tebrikler! Tüm seviyeleri tamamladiniz.");
            return;
        }

        SceneManager.LoadScene("Level" + levelNumber);

    }
}
