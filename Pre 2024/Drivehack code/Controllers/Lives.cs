using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public int maxLives = 3;
    public int lives = 0 ;

    public void LifeTake()
    {
        Debug.Log("-1 life");
        lives--;

        GameMaster.instance.UI.UpdateLives(lives);

        if (lives <= 0)
        {
            GameMaster.instance.LC.LevelFailed();
        }
    }
    public void LifeAdd(int amount)
    {

        lives += amount;
        lives = Mathf.Min(lives,maxLives);
        GameMaster.instance.UI.UpdateLives(lives);
    }
}
