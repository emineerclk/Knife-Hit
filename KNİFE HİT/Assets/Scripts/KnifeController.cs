using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class KnifeController : MonoBehaviour
{
    [SerializeField] HolderActivater holderActivater;
    ScoreManager scoremanager;
    [SerializeField] Rigidbody2D RdKnife;
    [SerializeField] private float moveSpeed;
    bool isThrown = false;
    bool isKnifeThrown = false;
    bool hasCollided = false;
    private int collisionCount = 0;
    private bool isGameOver = false;

    private object watermelonTransform;
    public  AudioClip hit;



    private void Start()
    {
        holderActivater = GameObject.FindWithTag("holder").GetComponent<HolderActivater>();
        scoremanager = GameObject.FindWithTag("score").GetComponent<ScoreManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isThrown)
            {
                isThrown = true;
                YukarıGit();
            }
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!isThrown)
                {
                    isThrown = true;
                    YukarıGit();
                }
            }
        }
    }

    void YukarıGit()
    {
        RdKnife.AddForce(Vector2.up * moveSpeed * Time.fixedDeltaTime * 100f);

    }
    void OnCollisionEnter2D(Collision2D other)

    {
        if (other.gameObject.CompareTag("Watermelon"))
        {   
            AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position, 1f);
            scoremanager.IncreaseScore();
            LevelContoller.Instance.thrownKnife++;
            
            holderActivater.DeactiviteHolder();


            isKnifeThrown = true;

            RdKnife.isKinematic = true;

            transform.SetParent(other.gameObject.transform);

            KnifeManager.Instance.knifeCount++;

            LevelContoller.Instance.atilanBicak++;

            if (!LevelContoller.Instance.CheckGameOver())
            {
                KnifeManager.Instance.AddNewKnife();
            }


            if (LevelContoller.Instance.atilanBicak == LevelContoller.Instance.GetknifeNumber())
            {
                EnableChildRigidbodies(other.gameObject);

                Watermelon.Instance.LevelEnd();
            }
        }

        if (isGameOver)
            return;

        if (other.gameObject.tag == "knife")
        {

            if (!isKnifeThrown)
            {
                Destroy(gameObject);
                Health.Instance.DecreaseHealth();
            }

        }
    }
    void EnableChildRigidbodies(GameObject parentObject)
    {
        int childCount = parentObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {

            GameObject childObject = parentObject.transform.GetChild(i).gameObject;
            Rigidbody2D childRigidbody = childObject.GetComponent<Rigidbody2D>();

            if (childRigidbody != null)
            {
                if (childObject.CompareTag("knife"))
                {
                    childObject.GetComponent<Collider2D>().enabled = false;
                    childRigidbody.isKinematic = false;
                    childRigidbody.gravityScale = 1f;
                }
                else
                {
                    childRigidbody.gravityScale = 0f;
                }
                childRigidbody.simulated = true;
            }
        }
    }
}







 