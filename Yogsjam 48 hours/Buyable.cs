using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "buyable", menuName = "Store/buyable")]
public class Buyable : ScriptableObject
{
    public int Buycost = 0;
    public GameObject crate;
    public Defence defence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
