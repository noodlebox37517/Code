using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public bool krampusactive;
    public GameObject krampusprefab;
    public GameObject Krampusactual;
    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        krampusactive = true;
           Krampusactual = Instantiate<GameObject>(krampusprefab, spawnpoint.position, Quaternion.identity);
    }
}
