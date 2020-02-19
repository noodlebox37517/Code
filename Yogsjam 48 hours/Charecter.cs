using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    public Health health;
    // Start is called before the first frame update
    void Awake()
    {
        if(this.GetComponent<Health>() == null)
        {
            health = this.gameObject.AddComponent<Health>();
        }
        else
        {
            health = this.GetComponent<Health>();
        }

    }
    public virtual void Death()
    {
        Destroy(this.gameObject);
        Debug.Log(this.name + " died");
    }


}
