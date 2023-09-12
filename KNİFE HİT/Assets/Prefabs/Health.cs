using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Health : MonoBehaviour
{
  public static Health Instance;

  [SerializeField] GameObject health;


   private int playerHealth = 3;
 

   private void Awake() 
   {

      if(Instance == null) {
          Instance = this;
          DontDestroyOnLoad(gameObject);
      }
      else 
          Destroy(this.gameObject);   
    }


   public void DecreaseHealth()
    {
      playerHealth--;

    if (playerHealth <=0)
      {
        Debug.Log("Oyun Bitti");
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(2);

        
      }
      else 
      {
         KnifeManager.Instance.AddNewKnife();
      }
    }

     public void IncreaseHealt()
     {
        playerHealth++;
     }

     public int GetPlayerHealt()
     {
        return playerHealth;
     }

     public void SetHealth(int num) {
      playerHealth = num;
     }

   
}
