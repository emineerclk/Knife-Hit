using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    public static KnifeManager Instance;

    [SerializeField] GameObject knife;
    [SerializeField] GameObject knifeıcon;

    //a=active
    //b=not active;
    [SerializeField] private Color aColor;
    [SerializeField] private Color bColor;

    public int knifeNumber = 7;

    public int knifeCount = 0;

    void Awake()
    {
        Instance = this;
    }
    public void AddNewKnife()
    {
        GameObject newKnife = Instantiate(knife, transform.position, Quaternion.identity);
    }
}