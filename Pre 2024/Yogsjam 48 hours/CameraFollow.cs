using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool ovveride = false;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Game.instance.santa.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Game.instance.santa.transform.position + offset;
    }
}
