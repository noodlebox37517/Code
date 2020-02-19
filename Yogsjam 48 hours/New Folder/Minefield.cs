using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minefield : MonoBehaviour
{
    public float minesperminute = 10f;
    public float Damage =10f;
    public float lastrespawn =0f;
    public List<GameObject> Mines;
    public List<GameObject> InactiveMines;

    private void Update()
    {

        //check if respawnmine
        if (Time.time> lastrespawn+(60/minesperminute))
        {
                lastrespawn = Time.time;
                if (InactiveMines.Count > 0) {
                    int minind = Random.Range(0, InactiveMines.Count);
                    InactiveMines[minind].SetActive(true);

                    InactiveMines.RemoveAt(minind);
            }
        }
    }
}
