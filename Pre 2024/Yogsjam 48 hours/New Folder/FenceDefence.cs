using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceDefence : MonoBehaviour
{
    public float slowamount = .5f;
    public float Damagepersecond = 2f;
    //slows and damages people moiving through
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Player")
            other.GetComponent<Health>().Damage(Damagepersecond*Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            other.GetComponent<EnemyController>().speedMulti = slowamount;
        if (other.tag == "Player")
            other.GetComponent<Santa>().speedMulti = slowamount;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            other.GetComponent<EnemyController>().speedMulti = 1f;
        if (other.tag == "Player")
            other.GetComponent<Santa>().speedMulti = 1f;
    }
}
