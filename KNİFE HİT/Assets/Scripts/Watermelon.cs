
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour
{
    public static Watermelon Instance;

    [SerializeField] public float rotationSpeed = 45f;
    [SerializeField] private float changeDirectionInterval = 3f;

    private bool isClockwise = true;

    public bool SetRandomWatermelonHizi = false;

    private int toplamBicakSayisi;

    public int rotationDirection = 1;
    public int emineDirection = 1;
    
    public int levelCase;
    public Sprite newSprite;
    public Sprite new1Sprite;
    private bool spriteRenderer = true;


    private bool isPaused = false;
    private float pauseDuration = 0.1f;
    private float pauseTimer = 0f;
    private float moveDuration = 0.01f;
    private float moveTimer = 0f;

    [SerializeField] SpriteRenderer[] fruitParts;

    [SerializeField] Sprite[] orangeParts;
    [SerializeField] Sprite[] limeParts;
    [SerializeField] Sprite[] watermelonParts;


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("ChangeRotationDirection", changeDirectionInterval, changeDirectionInterval);
    }

    private void Update()
    {
        if (SetRandomWatermelonHizi)
        {
            if (levelCase == 0)
            {
                transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            }
            else if (levelCase == 1)
            {
                transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            }
            else if (levelCase == 2)
            {
                transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            }
            else if (levelCase == 3)
            {
                transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            }
            else if (levelCase == 4)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer >= 0.5)
                {
                    moveTimer = 0f;
                    isPaused = !isPaused;
                    emineDirection = emineDirection * -1;
                    Debug.Log(isPaused);
                }

                if (isPaused)
                {

                    pauseTimer += Time.deltaTime;
                    if (pauseTimer >= 0.01f)
                    {
                        pauseTimer = 0f;
                    }
                }

                if (!isPaused)
                    transform.Rotate(0f, 0f, -rotationSpeed * emineDirection * Time.deltaTime);
            }
            else if (levelCase == 5)
            {
                transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                if (newSprite != null)
                {
                    Watermelon.Instance.GetComponent<SpriteRenderer>().sprite = newSprite;
                }
            }
            else if (levelCase == 6)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveDuration)
                {
                    moveTimer = 0f;

                    transform.Rotate(0f, 0f, -rotationSpeed * emineDirection * Time.deltaTime);
                }
            }
            else if (levelCase == 7)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer >= 0.5)
                {
                    moveTimer = 0f;
                    isPaused = !isPaused;
                    emineDirection = emineDirection * -1;
                    Debug.Log(isPaused);
                }

                if (isPaused)
                {
                    pauseTimer += Time.deltaTime;
                    if (pauseTimer >= 0.01f)
                    {
                        pauseTimer = 0f;
                    }
                }

                if (!isPaused)
                    transform.Rotate(0f, 0f, rotationSpeed * emineDirection * Time.deltaTime);
            }
            else if (levelCase == 8)
            {
                transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
                if (new1Sprite != null)
                {
                    Watermelon.Instance.GetComponent<SpriteRenderer>().sprite = new1Sprite;
                }
            }
            else if (levelCase==9)
            {    moveTimer += Time.deltaTime;
                 if (moveTimer >= moveDuration)
                {
                    moveTimer = 0f;
                    transform.Rotate(0f, 0f, -rotationSpeed * emineDirection * Time.deltaTime);
                }
            }
        }
    }

    public void ChangeFruitParts(int num)
    {
        if (num == 0)
        {
            for (int i = 0; i < fruitParts.Length; i++)
            {
                fruitParts[i].sprite = watermelonParts[i];
            }

        }
        else if (num == 1)
        {
            for (int i = 0; i < fruitParts.Length; i++)
            {
                fruitParts[i].sprite = orangeParts[i];
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < fruitParts.Length; i++)
            {
                fruitParts[i].sprite = limeParts[i];
            }
        }
    }
    private bool isLevelCompleted = false;

    public void SetToplamBicak(int num)
    {
        toplamBicakSayisi = num;
    }

    private void SetWatermelonRotate()
    {
        float rotation = rotationSpeed * Time.deltaTime;

        if (!isClockwise)
        {
            rotation = -rotation;
        }

        transform.Rotate(0f, 0f, rotation);
    }
    private void ChangeRotationDirection()
    {
        isClockwise = !isClockwise; // Yönü değiştir
    }

    private void DropWatermelonChildren()
    {
        Rigidbody2D[] childRigidbodies = transform.GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D childRigidbody in childRigidbodies)
        {
            childRigidbody.simulated = true;
            childRigidbody.gravityScale = 1f;
        }
    }

    public void LevelEnd()
    {
    Debug.Log("Kodun içine girdik mi");
    Rigidbody2D[] childRigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();
    SpriteRenderer watermelonSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    watermelonSpriteRenderer.color = new Color(watermelonSpriteRenderer.color.r, watermelonSpriteRenderer.color.g, watermelonSpriteRenderer.color.b, 0f);

    DropWatermelonChildren();

    SetRandomWatermelonHizi = false;

    foreach (Rigidbody2D childRigidbody in childRigidbodies)
    {
        childRigidbody.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 2f)), ForceMode2D.Impulse);
        childRigidbody.simulated = true;
        childRigidbody.gravityScale = 1f;
    }

    StartCoroutine(LoadNextLevelAfterDelay());
}

IEnumerator LoadNextLevelAfterDelay()
{
    yield return new WaitForSeconds(2f); 
    LevelContoller.Instance.LevelComplete();
}
        
    public void ChangeSprite(Sprite spr)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spr;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Watermelon"))
        {
            Debug.Log("Kodun icine girdik mi");
            Transform watermelonTransform = collision.gameObject.transform;

            for (int i = 0; i < watermelonTransform.childCount; i++)
            {
                Transform child = watermelonTransform.GetChild(i);
                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;
            }

            LevelContoller.Instance.atilanBicak++;

            if (LevelContoller.Instance.atilanBicak >= toplamBicakSayisi)
            {
                Rigidbody2D[] childRigidbodies = watermelonTransform.GetComponentsInChildren<Rigidbody2D>();
                SpriteRenderer watermelonSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

                watermelonSpriteRenderer.color = new Color(watermelonSpriteRenderer.color.r, watermelonSpriteRenderer.color.g, watermelonSpriteRenderer.color.b, 0f);

                DropWatermelonChildren();

                foreach (Rigidbody2D childRigidbody in childRigidbodies)
                {
                    childRigidbody.simulated = true;
                    childRigidbody.gravityScale = 0f;
                }

            }
            if (LevelContoller.Instance.CheckGameOver() && !isLevelCompleted)
            {
                isLevelCompleted = true;
                LevelContoller.Instance.LevelComplete();
            }
        }
    }
    
}




