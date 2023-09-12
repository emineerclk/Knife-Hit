using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderActivater : MonoBehaviour
{

    public GameObject[] knifeHolders;

    void Start()
    {
        
    }

    public void ActivateHolders(int knifeAmount){
        for(int i = 0; i < knifeAmount; i++){
            knifeHolders[i].SetActive(true);
        }
    }

    public void DeactiviteHolder()
    {
        int index = LevelContoller.Instance.knifeCount -  LevelContoller.Instance.thrownKnife;
        knifeHolders[index].SetActive(false);
    }
}
