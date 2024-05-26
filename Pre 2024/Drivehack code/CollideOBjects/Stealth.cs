using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    public Material invismat;
    public Material orig;
    public float revealDistance = 5f;
    private bool dark= false;
    // Start is called before the first frame update
    void Start()
    {
        GoDark();
    }

    // Update is called once per frame
    void Update()
    {

        if (dark)
        {
            float dist = this.transform.position.z;

            if (dist <= revealDistance)
            {
                dark = false;
                this.gameObject.GetComponent<MeshRenderer>().material = orig;
            }

        }
    }
    public void GoDark()
    {
        dark = true;
           orig = this.gameObject.GetComponent<MeshRenderer>().material;
        this.gameObject.GetComponent<MeshRenderer>().material = invismat;
    }
}
